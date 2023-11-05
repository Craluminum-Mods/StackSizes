using System.Collections.Generic;
using System.Linq;
using StackSizesMod.Configuration;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace StackSizesMod;

public static class Patches
{
    private static bool IsAllowed<T>(T obj)
    {
        return obj is not BlockMultiblock;
    }

    public static void ApplyPatches(this ICoreAPI api, Config config)
    {
        if (config.StackSizeMultiplier is not 0 and not 1.0f)
        {
            foreach (CollectibleObject obj in api.World.Collectibles)
            {
                obj.MaxStackSize = (int)(obj.MaxStackSize * config.StackSizeMultiplier);
            }

            return;
        }

        if (config.BlockStackSizes?.Count != 0)
        {
            foreach ((string key, int value) in config.BlockStackSizes)
            {
                Block block = api.World.Blocks.FirstOrDefault(x => x.Code == new AssetLocation(key));

                if (block == null || block.Code == null)
                {
                    continue;
                }

                block.MaxStackSize = value;
            }
        }

        if (config.ItemStackSizes?.Count != 0)
        {
            foreach ((string key, int value) in config.ItemStackSizes)
            {
                Item item = api.World.Items.FirstOrDefault(x => x.Code == new AssetLocation(key));

                if (item == null || item.Code == null)
                {
                    continue;
                }

                item.MaxStackSize = value;
            }
        }
    }

    public static Dictionary<string, int> GetDefaultStackSizesForBlocks(this ICoreAPI api)
    {
        Dictionary<string, int> stackSizes = new();

        foreach (Block block in api.World.Blocks)
        {
            if (!IsAllowed(block))
            {
                continue;
            }

            string code = block.Code?.ToString();
            if (!string.IsNullOrEmpty(code) && !stackSizes.ContainsKey(code))
            {
                stackSizes.Add(code, block.MaxStackSize);
            }
        }

        return stackSizes;
    }

    public static Dictionary<string, int> GetDefaultStackSizesForItems(this ICoreAPI api)
    {
        Dictionary<string, int> stackSizes = new();

        foreach (Item item in api.World.Items)
        {
            string code = item.Code?.ToString();
            if (!string.IsNullOrEmpty(code))
            {
                stackSizes.Add(code, item.MaxStackSize);
            }
        }

        return stackSizes;
    }
}