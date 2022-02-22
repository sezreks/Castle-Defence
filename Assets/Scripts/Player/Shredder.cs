using Assets.Scripts.WorldEnv.Bricks.Base;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    public Transform owner;
    public ParticleSystem Wood, Stone, Metal;

    private void Start()
    {
        owner = gameObject.transform.parent.parent;

    }
    private void OnTriggerEnter(Collider other)
    {
        var collectbricks = owner.GetComponent<CollectBricks>();
        collectbricks.GetBricksFromGround(other);
        if (other.GetComponent<Brick>() != null)
        {
            var particleeffect = other.GetComponent<Brick>().Name;

            if (particleeffect == "MetalBrick")
            {
                Metal.Play();
            }
            else if (particleeffect == "StoneBrick")
            {
                Stone.Play();
            }
            else if (particleeffect == "WoodBrick")
            {
                Wood.Play();
            }
            else
            {
                return;
            }
        }




    }

}
