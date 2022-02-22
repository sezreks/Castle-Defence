using Assets.Scripts.Components;
using Extentions;
using UnityEngine;


namespace Assets.Scripts.WorldEnv.Blocks.Base
{
    public abstract class Block : MonoBehaviour
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }

        protected Transform Parent;

        [SerializeField] private ParticleSystem blowUp;
        public Block(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public abstract void SetParent(Transform _parent);

        public virtual void SetActivate(Transform _parent)
        {
            gameObject.SetActive(true);
            SetParent(_parent);
            transform.parent = Parent;
            transform.localPosition = Vector3.zero.WithY(Parent.childCount * 0.14f);



            var index = transform.parent.GetSiblingIndex();
            for (int i = 0; i < index; i++)
                Parent.parent.GetChild(i).transform.localPosition += Vector3.zero.WithY(0.14f);


        }

        public virtual void Kill()
        {
            if (Health <= 0)
            {

                gameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
                TowerPoolManager.Instance.SetObject(Name, gameObject);
                Parent.root.GetComponent<TowerBuild>().blocksList.Remove(gameObject);
                return;
            }


        }

        public void GetDamage()
        {

            Health -= 1;
            Kill();
            return;


        }

    }
}
