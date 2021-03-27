using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headingTxt;

    public void Close()
    {
        Destroy(gameObject);
    }
}
