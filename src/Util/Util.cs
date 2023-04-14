using System.Collections.Generic;
using HarmonyLib;
using Vintagestory.API.Common;

namespace StackSizesMod;

public static class Util
{
    public static T GetField<T>(this object instance, string fieldname) => (T)AccessTools.Field(instance.GetType(), fieldname).GetValue(instance);

    public static bool Is(this CollectibleObject obj, string key) => obj.Code.ToString().Contains(key);

    public static bool DoesBlockDropItself(this CollectibleObject obj, ICoreAPI api)
    {
        if (obj is Item) return true;
        var block = obj as Block;

        try
        {
            ItemStack dropStack = block?.OnPickBlock(api.World, null);
            if (dropStack?.Block?.Code == obj?.Code) return true;
        }
        catch (System.Exception)
        {
            if (block?.Drops != null && block?.Drops?.Length != 0 && block?.Drops?[0]?.Code == block.Code)
            {
                return true;
            }
        }

        return false;
    }

    public static CollectibleObject GetCollectible(this ICoreAPI api, KeyValuePair<string, int> stackSize)
    {
        return api.World.GetItem(new AssetLocation(stackSize.Key)) as CollectibleObject ?? api.World.GetBlock(new AssetLocation(stackSize.Key));
    }
}