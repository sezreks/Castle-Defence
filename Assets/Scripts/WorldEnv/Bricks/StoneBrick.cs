using Assets.Scripts.WorldEnv.Bricks.Base;

namespace Assets.Scripts.WorldEnv.Bricks
{
    public class StoneBrick : Brick
    {
        const string key = "StoneBrick";
        const string Blockkey = "StoneBlock";
        public StoneBrick() : base(key, Blockkey)
        {
        }
    }
}
