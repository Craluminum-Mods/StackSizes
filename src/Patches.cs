using System.Collections.Generic;
using StackSizesMod.Configuration;
using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace StackSizesMod;

public static class Patches
{
    public static void ApplyPatches(this ICoreAPI api, StackSizesConfig config)
    {
        if (config.StackSizes?.Count == 0) return;

        foreach (var stackSize in config.StackSizes)
        {
            var obj = api.GetCollectible(stackSize);
            if (obj == null) continue;
            if (obj.Code == null) continue;

            obj.MaxStackSize = stackSize.Value;
        }
    }

    public static StackSizesConfig FillConfig(this ICoreAPI api, StackSizesConfig config)
    {
        if (config.StackSizes == null) return config;

        foreach (var obj in api.World.Collectibles)
        {
            if (obj?.Code == null) continue;
            if (obj?.Id == 0) continue;

            var code = obj?.Code?.ToString();
            if (config.StackSizes.ContainsKey(code)) continue;
            if (!obj.IsAllowed(api)) continue;

            config.StackSizes.Add(code, obj.MaxStackSize);
        }

        return config;
    }

    private static bool IsAllowed(this CollectibleObject obj, ICoreAPI api)
    {
        if (obj.Code.EndVariant() == "snow")
            return false;

        if (obj is ItemCreature || obj is BlockForFluidsLayer || obj is BlockMultiblock || obj is ItemWearable)
            return false;

        if (obj.Tool != null)
            return false;

        if (obj.Class == "ItemLiquidPortion")
            return false;

        if (obj.Attributes?["backpack"].Exists == true)
            return false;

        if (obj is Item)
            return true;

        if (obj.Is("caveart") || obj.Is("ontree") || obj.Is("microblock") || obj.Is("creative") || obj.Is("stalagsection") || obj.Is("wildbeehive") || obj.Is("bunchocandles"))
            return false;

        var numbers1to7 = new[] { "1", "2", "3", "4", "5", "6", "7" };
        if ((obj.Is("sand") || obj.Is("gravel")) && numbers1to7.Contains(obj.Code.EndVariant()))
            return false;

        if (!obj.DoesBlockDropItself(api))
        {
            return false;
        }

        return true;
    }
}