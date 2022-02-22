using Assets.Scripts.Components;
using Extentions;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private float sendPower;

    [SerializeField] private GameObject muzzle;

    [SerializeField] private GameObject tower;

    public GameObject enemy;

    public List<Transform> spawnedBalls;
    private float timer;
    private float randomTime;

    // Start is called before the first frame update
    void Start()
    {
        randomTime = 3;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.fixedDeltaTime;
        if (timer >= randomTime)
        {
            var randomFloat = Random.Range(-.65f, .65f);
            var cannonball = CannonPoolManager.Instance.GetObject("Cannonball");
            var position = muzzle.transform.position + Vector3.zero.WithX(randomFloat);

            spawnedBalls.Add(cannonball.transform);
            if (enemy != null && enemy.TryGetComponent<EnemySM>(out EnemySM esm))
            {

                esm.aimState.target = cannonball.transform;

                if (esm.GetCurrentState() == esm.lostState)
                {
                    this.enabled = false;
                }
            }

            cannonball.transform.position = position;
            cannonball.GetComponent<Cannonball>().tower = tower;
            cannonball.GetComponent<Cannonball>().myCannon = gameObject;
            cannonball.SetActive(true);
            cannonball.GetComponent<Rigidbody>().AddForce(muzzle.transform.forward * -sendPower, ForceMode.Impulse);

            randomTime = 3.1f;

        }

        if (timer > 3)
        {
            randomTime = Random.RandomRange(1.5f, 3f);
            timer = 0;
        }
    }

}
