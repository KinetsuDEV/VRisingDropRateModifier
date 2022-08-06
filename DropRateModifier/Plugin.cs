﻿using BepInEx;
using BepInEx.IL2CPP;
using DropRateModifier.Configs;
using HarmonyLib;
using System.Reflection;
using VRisingUtils.Utils;
using Wetstone.API;

namespace DropRateModifier
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("xyz.molenzwiebel.wetstone")]
    public class Plugin : BasePlugin
    {
        private Harmony harmony;

        public override void Load()
        {
            if (!VWorld.IsServer)
            {
                Log.LogWarning($"Plugin {PluginInfo.PLUGIN_NAME} is server side only");
                return;
            }

            LogUtils.Initialize(Log);
            DropRateConfig.Initialize(Config);

            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} {PluginInfo.PLUGIN_VERSION} loaded successfully!");
        }

        public override bool Unload()
        {
            if (!VWorld.IsServer)
                return true;

            Config.Clear();
            harmony.UnpatchSelf();

            return true;
        }
    }
}
