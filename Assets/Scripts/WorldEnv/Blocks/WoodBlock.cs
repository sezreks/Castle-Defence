using Assets.Scripts.WorldEnv.Blocks.Base;
using UnityEngine;

namespace Assets.Scripts.WorldEnv.Blocks
{
    public class WoodBlock : Block
    {
        const string key = "WoodBlock";
        public WoodBlock() : base(key, 2)
        {
        }

        public override void SetParent(Transform _parent)
        {
            Parent = _parent.Find("WoodPlaceholder").transform;
        }
    }
}
