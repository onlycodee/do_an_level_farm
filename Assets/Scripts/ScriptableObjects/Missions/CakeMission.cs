//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//[CreateAssetMenu()]
//public class CakeMission : MissionBase
//{
//    [SerializeField] ItemHolder[] requiredItems;
//    public override bool CheckIfCompleted()
//    {
//        foreach (var requiredItem in requiredItems)
//        {
//            if (!IsItemCompleted(requiredItem)) 
//            {
//                Debug.LogError("Cake mission not complete: " + requiredItem.InventoryItem.name);
//                return false;
//            } 
//        }
//        return true;
//    }
//    public bool IsItemCompleted(ItemHolder cake)
//    {
//        Debug.LogError("Inventory count: " + Inventory.Instance.GetAllItems());
//        foreach (var item in Inventory.Instance.GetAllItems())
//        {
//            if (item.InventoryItem.Id == cake.InventoryItem.Id)
//            {
//                if (item.Quantity >= cake.Quantity) return true;
//                return false;
//            }
//        }
//        return false;
//    }

//    public override ItemHolder[] GetItems()
//    {
//        return requiredItems;
//    }

//    //public override void UpdateUI()
//    //{
//    //    throw new System.NotImplementedException();
//    //}
//}
