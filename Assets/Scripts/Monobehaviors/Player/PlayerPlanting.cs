using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [SerializeField] Crop cropToPlantPrefab;
    [SerializeField] LayerMask fieldLayermark;
    [SerializeField, Range(1f, 10f)] float plantingRange;
    [SerializeField] CropFactory cropFactory;
    [SerializeField] Color fieldHighlightColor;

    Camera mainCamera;
    Collider[] inPlantingRangeColliders = new Collider[100];
    Collider lastClosestField = null;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        int collidersCnt = Physics.OverlapSphereNonAlloc(transform.position, plantingRange, inPlantingRangeColliders, fieldLayermark, QueryTriggerInteraction.Collide);
        if (collidersCnt > 0)
        {
            Collider curClosestField = GetClosestField(collidersCnt);
            if (lastClosestField != null && lastClosestField != curClosestField)
            {
                lastClosestField.GetComponent<Renderer>().material.color = Color.white;
            }
            lastClosestField = curClosestField;
            lastClosestField.GetComponent<Renderer>().material.color = fieldHighlightColor;
            Field curFieldComp = lastClosestField.GetComponent<Field>();
            if (Input.GetMouseButtonDown(0))
            {
                if (curFieldComp.HasCrop)
                {
                    curFieldComp.Interac();
                    return;
                } 
                Crop cropInstance; 
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    cropInstance = cropFactory.GetCrop(CropType.BEET); 
                } 
                else if (Input.GetKey(KeyCode.Alpha2))
                {
                    cropInstance = cropFactory.GetCrop(CropType.CORN); 
                } 
                else if (Input.GetKey(KeyCode.Alpha3))
                {
                    cropInstance = cropFactory.GetCrop(CropType.WHEAT); 
                } 
                else
                {
                    cropInstance = cropFactory.GetCrop(CropType.GREENPLANT); 
                }
                curFieldComp.SetCrop(cropInstance);
                cropInstance.transform.SetParent(curClosestField.transform);
                cropInstance.transform.localPosition = Vector3.zero;
                cropInstance.transform.localScale = Vector3.zero;
                cropInstance.StartPlanting();
            } 
        } else
        {
            if (lastClosestField != null)
            {
                lastClosestField.GetComponent<Renderer>().material.color = Color.white;
                lastClosestField = null;
            }
        }
    }

    private Collider GetClosestField(int collidersCnt)
    {
        float minDistance = float.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < collidersCnt; i++)
        {
            Vector3 currentColliderPos = inPlantingRangeColliders[i].transform.position;
            float curDistance = Vector3.Distance(transform.position, currentColliderPos);
            if (minDistance > curDistance)
            {
                minDistance = curDistance;
                minIndex = i;
            }
        }
        return inPlantingRangeColliders[minIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, plantingRange);
    }
}
