using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI startgame;

    public float FadeInTime = 0.5f;

    public float StayTime = 0.8f;

    public float FadeOutTime = 0.7f;

    private float _timeChecker = 0;

    private Color _color;

    


    // Start is called before the first frame update
    void Start()
    {
        
        _color = startgame.color;

    }

    // Update is called once per frame
    void Update()
    {

        _timeChecker += Time.deltaTime;
        if (_timeChecker < FadeInTime)
        {
            startgame.color = new Color(_color.r, _color.g, _color.b, _timeChecker / FadeInTime);

        }
        else if (_timeChecker < FadeInTime + StayTime)
        {
            startgame.color = new Color(_color.r, _color.g, _color.b, 1);

        }
        else if (_timeChecker <FadeInTime+ StayTime + FadeOutTime)
        {
            startgame.color = new Color(_color.r, _color.g, _color.b, 1 - (_timeChecker - (FadeInTime + StayTime)) / FadeOutTime);

        }
        else
        {
            _timeChecker = 0;
        }
      

    }

 
}
