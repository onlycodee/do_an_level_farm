using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] CanvasGroup overlay;
    [SerializeField] float duration = .5f;

    public IEnumerator FadeIn()
    {
        overlay.blocksRaycasts = true;
        overlay.interactable = true;
        yield return overlay.DOFade(1, duration);
    }
    public IEnumerator FadeOut()
    {
        yield return overlay.DOFade(0, duration);
        overlay.blocksRaycasts = false;
        overlay.interactable = false;
    }
}
