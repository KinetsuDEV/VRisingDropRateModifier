using BepInEx.Configuration;

namespace DropRateModifier.Configs
{
    internal static class DropRateConfig
    {
        internal static ConfigEntry<float> DropRateModifier { get; private set; }

        public static void Initialize(ConfigFile config)
        {
            DropRateModifier = config.Bind(nameof(DropRateConfig), nameof(DropRateModifier), 1.0f, "Drop rate modifier value");
        }
    }
}
