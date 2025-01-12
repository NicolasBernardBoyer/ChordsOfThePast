using System.Collections;
using UnityEngine;

public class Harp : MonoBehaviour
{
    private bool isSelected = false;
    public bool isChosenHarp;

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play(); 
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
        if (!selected)
        {
            GetComponent<SpriteRenderer>().color = new Color(100f / 255f, 100f / 255f, 100f / 255f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        }
    }

    public bool GetIsSelected()
    {
        return isSelected; 
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut(1f));
    }

    private IEnumerator FadeOut(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }

        // Ensure we end up fully transparent
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
