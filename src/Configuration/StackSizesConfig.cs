using System.Collections.Generic;

namespace StackSizesMod.Configuration
{
    public class StackSizesConfig
    {
        public Dictionary<string, int> StackSizes = new() { };

        public StackSizesConfig() { }
        public StackSizesConfig(StackSizesConfig previousConfig)
        {
            foreach (var item in previousConfig.StackSizes)
            {
                if (StackSizes.ContainsKey(item.Key)) continue;
                StackSizes.Add(item.Key, item.Value);
            }
        }
    }
}
