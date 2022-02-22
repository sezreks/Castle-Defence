using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockablesScript : MonoBehaviour
{
    public List<Transform> unlockables;
    private Image curUnlockableImage;
    public GameObject levelBar;
    public List<GameObject> arenas;
    public static UnlockablesScript Instance;

    public Image arena1, arena2, arena3;
    // Start is called before the first frame update
    void OnEnable()
    {



        Instance = this;
        if (PlayerData.Instance.curArena == 0)
        {
            arena1.gameObject.SetActive(true);
            arena2.gameObject.SetActive(true);
            arena1.rectTransform.anchoredPosition = new Vector3(-600, 800, 0);
            arena2.rectTransform.anchoredPosition = new Vector3(600, 800, 0);
        }
        else if (PlayerData.Instance.curArena == 1)
        {

            arena2.gameObject.SetActive(true);
            arena3.gameObject.SetActive(true);
            arena2.rectTransform.anchoredPosition = new Vector3(-600, 800, 0);
            arena3.rectTransform.anchoredPosition = new Vector3(600, 800, 0);


        }
        else
        {
            arena3.gameObject.SetActive(true);
            arena3.rectTransform.anchoredPosition = new Vector3(-600, 800, 0);
        }



        var curLevelBar = (PlayerData.Instance.curLevel) % 4;
        if (curLevelBar == 0)
        {

            for (int i = 0; i < 4; i++)
            {
                levelBar.transform.GetChild(i).gameObject.SetActive(false);


            }
            levelBar.transform.GetChild(0).gameObject.SetActive(true);



        }
        else
        {
            for (int i = 0; i <= curLevelBar; i++)
            {
                levelBar.transform.GetChild(i).gameObject.SetActive(true);
            }
        }


    }

    private void OnDisable()
    {

    }

    // Update is called once per frame





}

