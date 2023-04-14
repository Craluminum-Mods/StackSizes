using Vintagestory.API.Common;

namespace StackSizesMod.Configuration;

static class ModConfig
{
    private const string jsonConfig = "ConfigureEverything/StackSizes.json";
    private static StackSizesConfig config;

    public static void ReadConfig(ICoreAPI api)
    {
        try
        {
            config = LoadConfig(api);
            config = api.FillConfig(config);

            if (config == null)
            {
                GenerateConfig(api);
                config = LoadConfig(api);
                config = api.FillConfig(config);
            }
            else
            {
                GenerateConfig(api, config);
                config = api.FillConfig(config);
            }
        }
        catch
        {
            GenerateConfig(api);
            config = LoadConfig(api);
            config = api.FillConfig(config);
        }

        api.ApplyPatches(config);
    }

    private static StackSizesConfig LoadConfig(ICoreAPI api) => api.LoadModConfig<StackSizesConfig>(jsonConfig);
    private static void GenerateConfig(ICoreAPI api) => api.StoreModConfig(new StackSizesConfig(), jsonConfig);
    private static void GenerateConfig(ICoreAPI api, StackSizesConfig previousConfig) => api.StoreModConfig(new StackSizesConfig(previousConfig), jsonConfig);
}