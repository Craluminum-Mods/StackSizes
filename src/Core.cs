using Vintagestory.API.Common;
using StackSizesMod.Configuration;

[assembly: ModInfo("Stack Sizes",
    Authors = new[] { "Craluminum2413" })]

namespace StackSizesMod;

class Core : ModSystem
{
    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

    public override void AssetsFinalize(ICoreAPI api)
    {
        // Reading it twice because it creates empty dictionary on first load
        ModConfig.ReadConfig(api);
        ModConfig.ReadConfig(api);
        api.World.Logger.Event("started 'Stack Sizes' mod");
    }
}