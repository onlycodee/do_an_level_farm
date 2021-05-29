using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenNest : MonoBehaviour
{
    [SerializeField] ChickenEgg eggPrefab;
    [SerializeField] BoxCollider nestBoundary;
    [SerializeField] BoxCollider modelBoundary;
    [SerializeField] Transform eggParent; 

    List<ChickenEgg> chickenEggs = new List<ChickenEgg>();
    bool isCollectingEggs = false;

    public void Test()
    {
        SpawnEgg(GetRandomPoint());
    }
    public void SpawnEgg(Vector3 pos)
    {
        Debug.Log("Spawn egg");
        pos.y = 2;
        ChickenEgg newEgg = Instantiate(eggPrefab, eggParent);
        newEgg.transform.position = pos;//GetRandomPointInsideCollider(nestBoundary);
        chickenEggs.Add(newEgg);
    }
    public Vector3 GetRandomPoint()
    {
        return Utilities.GetRandomPointInsideCollider(nestBoundary);
    }
    public Collider GetModelBoudary()
    {
        return modelBoundary;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (chickenEggs.Count > 0)
            {
                if (isCollectingEggs) return;
                isCollectingEggs = true;
                ChickenEggItem chickenEggItem = chickenEggs[0].GetEggItem();
                int numEggs = chickenEggs.Count; 
                eggParent.gameObject.SetActive(false);
                if (FloatingUIItemController.Instance)
                {
                    //Camera mainCamera = Camera.main;
                    //Vector2 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
                    //RectTransform canvasRectTransform = FloatingUIItemController.Instance.GetComponent<RectTransform>();
                    //viewportPos.x = viewportPos.x * canvasRectTransform.rect.width - canvasRectTransform.rect.width / 2.0f;
                    //viewportPos.y = viewportPos.y * canvasRectTransform.rect.height - canvasRectTransform.rect.height / 2.0f;
                    FloatingUIItemController.Instance.ShowAnchored(
                        chickenEggItem.Avatar,
                        numEggs,
                        transform.position, 
                        150
                        , 1.0f, () =>
                        {
                            AddEggsToInventoryAndClear();
                        }); ;
                }
            }
        }
    }
    void AddEggsToInventoryAndClear()
    {
        eggParent.gameObject.SetActive(true);
        isCollectingEggs = false;
        Inventory.Instance.AddItem(chickenEggs[0].GetEggItem(), chickenEggs.Count);
        for (int i = 0; i < chickenEggs.Count; i++)
        {
            Destroy(chickenEggs[i].gameObject);
        }
        chickenEggs.Clear();
    }
}
