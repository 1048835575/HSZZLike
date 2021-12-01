using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
    public RawImage rawImage;
    public RectTransform rectTransform;
    [HideInInspector]
    public bool isBlack = false;
    [HideInInspector]
    public float FadeSpeed = 1;
    void Start()
    {
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        rawImage.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlack)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * FadeSpeed * 0.5f);
            if (rawImage.color.a < 0.1f)
            {
                rawImage.color = Color.clear;
            }
        }
        else
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.black, Time.deltaTime * FadeSpeed * 0.5f);
            if (rawImage.color.a > 0.9f)
            {
                rawImage.color = Color.black;
            }
        }
    }
    public void BackGroundContril(bool b)
    {
        if (b==true)
        {
            isBlack = true;
        }
        else
        {
            isBlack = false;
        }
    }
}
