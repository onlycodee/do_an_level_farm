using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ToastManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI notifyText; 
    public static ToastManager Instance = null;

    Vector3 initAnchoredPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        initAnchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    public void ShowNotify(string content)
    {

    }
}
