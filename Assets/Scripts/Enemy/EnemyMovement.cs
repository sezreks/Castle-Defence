using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private GameObject spawner;
    [SerializeField] private GameObject myTower;
    private bool goBuild;

    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        goBuild = false;
        rand = Random.Range(0, spawner.transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        var colbrick = gameObject.GetComponent<CollectBricks>();

        if (goBuild == false)
        {
            if (spawner.transform.GetChild(rand).childCount > 0)
            {
                var targetPos = spawner.transform.GetChild(rand).GetChild(0);
                var direction = (targetPos.transform.position - gameObject.transform.position).normalized;
                gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(direction.x, 0, direction.z) * 15 * Time.deltaTime);


            }
            else if (spawner.transform.GetChild(rand).childCount == 0)
            {
                rand = Random.Range(0, spawner.transform.childCount - 1);

                Debug.Log("Change target brick " + rand);

                return;

            }
            if(colbrick._collectedItems.Count >= 7)
            {

                goBuild = true;
            }

        }
        else if(goBuild == true)
        {

            var targetPos = myTower.transform.position;
            var direction = (targetPos - gameObject.transform.position).normalized;

            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(direction.x, 0, direction.z) * 30 * Time.deltaTime);

            if (colbrick._collectedItems.Count == 0) 
            {
                goBuild = false;
            }

            
           
        }


    }
}
