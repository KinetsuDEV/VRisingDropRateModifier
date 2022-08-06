using DropRateModifier.Configs;
using ProjectM;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using VRisingUtils.Utils;

namespace DropRateModifier.Systems
{
    internal static class DropRateSystem
    {
        internal static void ChangeDropRate()
        {
            if (DropRateConfig.DropRateModifier.Value == 1.0f)
                return;

            LogUtils.Logger.LogInfo($"Changing drop rate values. Modifier: {DropRateConfig.DropRateModifier.Value}");

            foreach (var dropTable in GetEntitiesDropTables())
            {
                var dropTableEntity = WetstoneUtils.PrefabLookupMap[dropTable.DropTableGuid];

                if (!WetstoneUtils.EntityManager.HasComponent<DropTableDataBuffer>(dropTableEntity))
                    continue;

                var buffer = WetstoneUtils.EntityManager.GetBuffer<DropTableDataBuffer>(dropTableEntity);
                var newBuffer = new List<DropTableDataBuffer>();

                foreach (var dropTableData in buffer)
                    newBuffer.Add(new DropTableDataBuffer()
                    {
                        DropRate = Math.Min(1, dropTableData.DropRate * DropRateConfig.DropRateModifier.Value),
                        ItemGuid = dropTableData.ItemGuid,
                        ItemType = dropTableData.ItemType,
                        Quantity = dropTableData.Quantity
                    });

                buffer.RemoveRange(0, buffer.Length);

                foreach (var dropTableData in newBuffer)
                    buffer.Add(dropTableData);
            }

            LogUtils.Logger.LogInfo($"Drop rate values changed successfully");
        }

        private static IList<DropTableBuffer> GetEntitiesDropTables()
        {
            var result = new List<DropTableBuffer>();
            var entities = WetstoneUtils.EntityManager.CreateEntityQuery(new EntityQueryDesc()
            {
                All = new ComponentType[] { ComponentType.ReadOnly<DropTableBuffer>() },
                Options = EntityQueryOptions.IncludeAll
            }).ToEntityArray(Allocator.Temp);

            foreach (var entity in entities)
                foreach (var dropTable in WetstoneUtils.EntityManager.GetBuffer<DropTableBuffer>(entity))
                    if (!result.Any(r => r.DropTableGuid == dropTable.DropTableGuid))
                        result.Add(dropTable);

            return result;
        }
    }
}
