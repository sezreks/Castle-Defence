using Assets.Scripts.Components;
using Assets.Scripts.WorldEnv.Blocks.Base;
using Extentions;
using TMPro;
using UnityEngine;
public class Cannonball : MonoBehaviour
{
    public int Health { get; protected set; }
    private TextMeshProUGUI healthText;
    public GameObject tower;
    private int placeholder = 3;
    [HideInInspector] public GameObject myCannon;
    public int minHealth, maxHealth;

    public Color[] colors;
    public Renderer colorMat;
    private int curColor = 0;
    [Range(0.0f, 1.0f)]
    public float colorSlider;

    private int startHealth;




    [System.Obsolete]


    void OnEnable()
    {
        for (int i = 0; gameObject.transform.localScale.x < 1f; i++)
        {
            gameObject.transform.localScale += new Vector3(.20f, .20f, .20f);
        }

        Health = Random.RandomRange(minHealth, maxHealth);
        startHealth = Health;
        colorSlider = 0f;
        healthText = transform.GetComponentInChildren<TextMeshProUGUI>();//  gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        healthText.transform.parent.SetParent(GameObject.Find("TMPHolder").transform);
        healthText.transform.parent.localScale = Vector3.one / 100;
        colorMat = transform.GetChild(0).GetComponent<Renderer>();
    }



    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetDamage();
        }

        if (colorSlider < 1)
        {

            colorMat.material.color = Color.Lerp(colors[curColor], colors[curColor + 1], colorSlider);
        }
        else
        {
            colorSlider = 0f;
            if (colors[curColor + 1] != null) { curColor++; }

        }





        healthText.transform.parent.position = transform.position + Vector3.zero.WithY(1f);
        healthText.transform.parent.eulerAngles = Vector3.zero;
        healthText.text = Health.ToString();

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<Cannonball>(out Cannonball component))
        {

            component.GetDamage();

        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == tower.name)
        {


            for (int i = 0; other.transform.GetChild(placeholder).childCount == 0 && placeholder != 1; i++)
            {
                placeholder--;
            }



            if (other.transform.GetChild(placeholder).childCount != 0 && other.transform.GetChild(placeholder).GetChild(0).TryGetComponent<Block>(out Block _block))
            {
                for (int x = 0; x < Health; x++)
                {
                    _block.GetDamage();
                    GetDamage();

                }
            }
            else
            {
                placeholder = 3;
                healthText.transform.parent.SetParent(transform);
                healthText.transform.parent.SetAsFirstSibling();
                CannonPoolManager.Instance.SetObject("Cannonball", gameObject);
                myCannon.GetComponent<CannonShoot>().spawnedBalls.Remove(transform);

            }
        }
    }

    public virtual void Kill()
    {
        if (Health <= 0)
        {
            tower.GetComponent<TowerBuild>().smokeCircle.GetComponent<ParticleSystem>().Play();
            healthText.transform.parent.SetParent(transform);
            healthText.transform.SetAsFirstSibling();
            curColor = 0;
            CannonPoolManager.Instance.SetObject("Cannonball", gameObject);
            myCannon.GetComponent<CannonShoot>().spawnedBalls.Remove(transform);
        }


    }

    public void GetDamage()
    {
        colorSlider += 1 / (startHealth - 1).ToFloat() * (colors.Length - 1);

        gameObject.transform.localScale -= new Vector3(0.07f, 0.07f, 0.07f);
        Health -= 1;
        Kill();
        return;


    }


}
