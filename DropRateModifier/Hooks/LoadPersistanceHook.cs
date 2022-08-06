using DropRateModifier.Systems;
using HarmonyLib;
using ProjectM;
using System;
using VRisingUtils.Utils;

namespace DropRateModifier.Hooks
{
    [Harmony]
    internal static class LoadPersistanceHook
    {
        [HarmonyPatch(typeof(LoadPersistenceSystemV2), nameof(LoadPersistenceSystemV2.SetLoadState))]
        [HarmonyPrefix]
        private static void LoadPersistenceSystemV2_SetLoadState_Prefix(ServerStartupState.State loadState)
        {
            try
            {
                if (loadState == ServerStartupState.State.SuccessfulStartup)
                    DropRateSystem.ChangeDropRate();
            }
            catch (Exception e)
            {
                LogUtils.Logger.LogError(e);
            }
        }
    }
}
