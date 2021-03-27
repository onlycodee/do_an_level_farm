using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] float timeToChangeCoinText = 1f;
    int currentCoin;
    CoinDisplay coinText;

    private void Awake()
    {
        coinText = FindObjectOfType<CoinDisplay>();
    }

    public int CurrentCoin
    {
        set
        {
            currentCoin = value;
            coinText.SetCoinText(currentCoin);
        }
        get
        {
            return currentCoin;
        }
    }

    public void AddCoin(int amount)
    {
        StartCoroutine(ChangeCoinText(currentCoin, currentCoin + amount, timeToChangeCoinText));
        currentCoin += amount;
    } 
    public void SubtractCoin(int amount)
    {
        StartCoroutine(ChangeCoinText(currentCoin, currentCoin - amount, timeToChangeCoinText));
        currentCoin -= amount;
    } 

    public bool HasEnoughCoint(int amount)
    {
        return currentCoin >= amount;
    }

    public IEnumerator ChangeCoinText(int startValue, int toValue, float time)
    {
        float timer = 0;
        coinText.SetCoinText(startValue);
        while (timer <= 1f)
        {
            timer += Time.deltaTime / time;
            int currentValue = (int)Mathf.Lerp(startValue, toValue, timer);
            coinText.SetCoinText(currentValue);
            yield return null;
        }
    }
}
