using Vintagestory.API.Common;
using StackSizesMod.Configuration;

[assembly: ModInfo(name: "Stack Sizes", modID: "stacksizes", Side = "Server")]

namespace StackSizesMod;

class Core : ModSystem
{
    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

    public override void AssetsFinalize(ICoreAPI api)
    {
        ModConfig.ReadConfig(api);
        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }
}