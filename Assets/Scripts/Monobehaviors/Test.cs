using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void AddCoint(int amount)
    {
        FindObjectOfType<CoinManager>().AddCoin(amount);
    }
    public void HelloWorld()
    {
        Debug.LogError("Hello World");
    }
}
