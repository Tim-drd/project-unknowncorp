using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    private float transparence;
    public static bool fadeOut;
    public float Step = 0.1f;
    public static int _time = 1;

    void Start()
    {
        transparence = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        transparence = Mathf.Clamp(transparence, 0, 1);
        if (fadeOut)
        {
            transparence += Step;
            _time++;
            if (_time % 50 == 0)
            {
                _time = _time % 50;
                fadeOut = false;
            }
        }
        else
        {
            transparence -= Step;
        }
        GetComponent<CanvasGroup>().alpha = transparence;
    }
}
