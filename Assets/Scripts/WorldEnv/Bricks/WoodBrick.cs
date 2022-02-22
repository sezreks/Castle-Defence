using Assets.Scripts.WorldEnv.Bricks.Base;

namespace Assets.Scripts.WorldEnv.Bricks
{
    public class WoodBrick : Brick
    {
        const string key = "WoodBrick";
        const string Blockkey = "WoodBlock";
        public WoodBrick() : base(key, Blockkey)
        {
        }
    }
}
