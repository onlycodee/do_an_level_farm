using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicePlant : Crop
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    protected override void Start()
    {
        StartPlanting();
    }
    private void Update()
    {
        if (isPlanted && transform.localScale.x <= 1)
        {
            transform.localScale += Vector3.one * Time.deltaTime / timeToGrow;
        }
    }
}
