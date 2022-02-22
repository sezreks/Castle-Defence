using Assets.Scripts.WorldEnv.Blocks.Base;
using UnityEngine;

namespace Assets.Scripts.WorldEnv.Blocks
{
    public class StoneBlock : Block
    {
        const string key = "StoneBlock";
        public StoneBlock() : base(key, 4)
        {
        }

        public override void SetParent(Transform _parent)
        {
            Parent = _parent.Find("StonePlaceholder").transform;
        }


    }
}
