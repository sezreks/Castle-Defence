using Assets.Scripts.Components;
using Assets.Scripts.WorldEnv.Blocks.Base;
using Assets.Scripts.WorldEnv.Bricks.Base;
using Extentions;
using UnityEngine;

namespace Assets.Scripts.WorldEnv.Blocks
{
    public class MetalBlock : Block
    {
        const string key = "MetalBlock";
        public MetalBlock() : base(key, 6)
        {
        } 
        
        
        
        public override void SetParent(Transform _parent)
        {
           Parent = _parent.Find("MetalPlaceholder").transform;
        }
    }
}
