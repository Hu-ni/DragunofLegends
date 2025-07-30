using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    [SerializeField]
    CanvasGroup fadeCanvas;

    void Start()
    {
        StartCoroutine(FadeIn());
    }


    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            fadeCanvas.alpha = t;
            yield return null;
        }

        SceneManager.LoadScene("WeaponSelectScene");
    }
}
