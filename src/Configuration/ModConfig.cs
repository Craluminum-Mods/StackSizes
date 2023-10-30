using Vintagestory.API.Common;

namespace StackSizesMod.Configuration;

static class ModConfig
{
    private const string jsonConfig = "ConfigureEverything/StackSizes.json";
    private static Config config;

    public static void ReadConfig(ICoreAPI api)
    {
        try
        {
            config = LoadConfig(api);

            if (config == null)
            {
                GenerateConfig(api);
                config = LoadConfig(api);
            }
            else
            {
                GenerateConfig(api, config);
            }
        }
        catch
        {
            GenerateConfig(api);
            config = LoadConfig(api);
        }

        api.ApplyPatches(config);
    }

    private static Config LoadConfig(ICoreAPI api)
    {
        return api.LoadModConfig<Config>(jsonConfig);
    }

    private static void GenerateConfig(ICoreAPI api)
    {
        api.StoreModConfig(new Config(api), jsonConfig);
    }

    private static void GenerateConfig(ICoreAPI api, Config previousConfig)
    {
        api.StoreModConfig(new Config(api, previousConfig), jsonConfig);
    }
}