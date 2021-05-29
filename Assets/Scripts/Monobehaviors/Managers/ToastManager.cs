using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class ToastManager : MonoBehaviour
{
    [SerializeField] FloatingNotification floatingNotifyPrefab; 

    public static ToastManager Instance = null;
    RectTransform rectTrans;
    Camera mainCamera;

    void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        Instance = this;
    }

    public void ShowNotify(string content, Vector3 initPosition, int moveUpAmount = 150, float duration = .75f, Action onCompleted = null)
    {
        Vector2 canvasPosition = mainCamera.WorldToViewportPoint(initPosition);
        canvasPosition.x = canvasPosition.x * rectTrans.rect.width - rectTrans.rect.width / 2.0f;
        canvasPosition.y = canvasPosition.y * rectTrans.rect.height - rectTrans.rect.height / 2.0f;

        FloatingNotification instance = Instantiate(floatingNotifyPrefab, transform);
        instance.SetContent(content);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = canvasPosition;
        rectTransform.DOAnchorPos(canvasPosition + Vector2.up * moveUpAmount, duration).SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                instance.GetComponent<CanvasGroup>().DOFade(.0f, .2f)
                .OnComplete(() =>
                {
                    onCompleted?.Invoke();
                    Destroy(instance.gameObject);
                });
            });
    }
}
