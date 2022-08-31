using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField] Color untintedColor;
    [SerializeField] Color tintedColor;
    Image image;
    float f;
    [SerializeField] float speed = 0.5f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void Tint()
    {
        f = 0f;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        f = 0f;
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while(f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f,0,1f);

            Color c = image.color;
            c = Color.Lerp(untintedColor, tintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
        
    }

    private IEnumerator UnTintScreen()
    {
        while(f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f,0,1f);

            Color c = image.color;
            c = Color.Lerp(tintedColor, untintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }

}
