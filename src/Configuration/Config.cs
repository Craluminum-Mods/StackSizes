using System.Collections.Generic;
using Vintagestory.API.Common;

namespace StackSizesMod.Configuration
{
    public class Config
    {
        public Dictionary<string, int> BlockStackSizes { get; set; } = new();
        public Dictionary<string, int> ItemStackSizes { get; set; } = new();

        public Config() { }

        public Config(ICoreAPI api)
        {
            FillDefault(api);
        }

        public Config(ICoreAPI api, Config previousConfig)
        {
            foreach ((string key, int value) in previousConfig.BlockStackSizes)
            {
                if (!BlockStackSizes.ContainsKey(key))
                {
                    BlockStackSizes.Add(key, value);
                }
            }

            foreach ((string key, int value) in previousConfig.ItemStackSizes)
            {
                if (!ItemStackSizes.ContainsKey(key))
                {
                    ItemStackSizes.Add(key, value);
                }
            }

            FillDefault(api);
        }

        private void FillDefault(ICoreAPI api)
        {
            foreach ((string key, int value) in api.GetDefaultStackSizesForBlocks())
            {
                if (!BlockStackSizes.ContainsKey(key))
                {
                    BlockStackSizes.Add(key, value);
                }
            }

            foreach ((string key, int value) in api.GetDefaultStackSizesForItems())
            {
                if (!ItemStackSizes.ContainsKey(key))
                {
                    ItemStackSizes.Add(key, value);
                }
            }
        }
    }
}
