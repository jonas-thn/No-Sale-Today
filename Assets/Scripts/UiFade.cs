using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiFade : MonoBehaviour
{
    [SerializeField] Image blackImage;

    [SerializeField] float fadeTime = 2f;
    float timer = 2;
    float alpha = 1;

    bool canFade = true;

    public IEnumerator FadeIn()
    {
        if (canFade)
        {
            timer = 0;
            canFade = false;

            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                alpha = timer / fadeTime;
                blackImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            timer = fadeTime;
            alpha = 1;
            blackImage.color = new Color(0, 0, 0, alpha); 

            canFade = true;
        }
    }

    public IEnumerator FadeOut()
    {
        if (canFade)
        {
            canFade = false;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                alpha = timer / fadeTime;
                blackImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            timer = 0;
            alpha = 0;
            blackImage.color = new Color(0, 0, 0, alpha);

            canFade = true;
        }
    }
}
