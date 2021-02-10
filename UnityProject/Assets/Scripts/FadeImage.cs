using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeImage : MonoBehaviour {

    public int speed;
    bool On;
    
    IEnumerator FadeTo(float aValue, float aTime)
    {
        Color imageColor = transform.GetComponent<Image>().color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * aTime)
        {
            Color changeColor = new Color(imageColor.r, imageColor.g, imageColor.b, Mathf.Lerp(imageColor.a, aValue, t));
            transform.GetComponent<Image>().color = changeColor;
            if (t >= 0.9)
            {
                t = 1;
                if (On)
                {
                    Color newColor = new Color(imageColor.r, imageColor.g, imageColor.b, 1);
                    transform.GetComponent<Image>().color = newColor;
                }
                else
                {
                    Color newColor = new Color(imageColor.r, imageColor.g, imageColor.b, 0);
                    transform.GetComponent<Image>().color = newColor;
                    transform.GetComponent<Image>().enabled = false;
                }
               
            }
            else
            {
                transform.GetComponent<Image>().enabled = true;
            }
            yield return null;
        }

    }
    public void FadeOut()
    {
        On = false;
        StartCoroutine(FadeTo(0.0f, speed));
    }
    public void FadeIn()
    {

        On = true;
        StartCoroutine(FadeTo(1.0f, speed));
        
    }
    public void SetAlpha(float alpha)
    {
        Color newColor = new Color(1, 1, 1, alpha);
        transform.GetComponent<Image>().color = newColor;
    }

}
