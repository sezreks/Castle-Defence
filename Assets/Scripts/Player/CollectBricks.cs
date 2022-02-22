using Assets.Scripts.Bases;
using Assets.Scripts.Components;
using Assets.Scripts.WorldEnv;
using Assets.Scripts.WorldEnv.Bricks.Base;
using MoreMountains.NiceVibrations;
using System.Collections.Generic;
using UnityEngine;


public class CollectBricks : Singleton<CollectBricks>
{
    public Transform collectibles;
    public List<Transform> _collectedItems;

    public void Start()
    {
        _persistent = false;
        _collectedItems = new List<Transform>();
    }

    public void GetBricksFromGround(Collider collision)
    {


        if (_collectedItems.Count < 20)
        {
            if (collision.TryGetComponent(out Brick item))
            {
                var blockHp = HpPoolManager.Instance.GetObject(item.Name);
                blockHp.transform.eulerAngles = new Vector3(0, 180, 0);
                blockHp.transform.position = item.transform.position;
                blockHp.SetActive(true);


                Spawner.Instance.GenerateItem(collision.transform.parent.GetComponent<BoxCollider>());
                collision.transform.parent = collectibles.GetChild((_collectedItems.Count));
                collision.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(item.MoveToTarget(collectibles));
                _collectedItems.Insert(0, collision.transform);
                Extentions.TasksExtentions.DoActionAfterSecondsAsync(() => { HpPoolManager.Instance.SetObject(item.Name, blockHp); }, 0.8f);



                if (gameObject.TryGetComponent<EnemySM>(out EnemySM eSM))
                {
                    eSM.speed = 3.5f;
                    Extentions.TasksExtentions.DoActionAfterSecondsAsync(() => { eSM.speed = 5f; }, 0.8f);
                }

                if (gameObject.TryGetComponent<PlayerSM>(out PlayerSM pSM))
                {
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    pSM.speed = 5;
                    Extentions.TasksExtentions.DoActionAfterSecondsAsync(() => { pSM.speed = 8f; }, 0.8f);
                }






            }
        }
        else
        {

            // Ekrana uyari ver
            Debug.Log("CAN'T CARRY MORE YOU IDIOT DROP SOME BRICKS YOU ARE NOT SUPERMAN");
        }


    }


}
