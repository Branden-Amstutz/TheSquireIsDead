using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Transform Item  { get; private set; }
    public enum InventoryState 
    {
        empty,
        ocuppied
    }

    public InventoryState State { get; private set; }

    public bool isMainHand;

    public void AddItem(Transform newItem)
    {
        if(Item == null)
        {
            Item = newItem;
            State = InventoryState.ocuppied;
        }
        else
        {
            Debug.LogError("ERROR: INVENTORY ALREADY OCCUPIED");
        }
    }
    public void RemoveItem()
    {
        if(Item != null)
        {
            Item = null;
            State = InventoryState.empty;
        }
        else
        {
            Debug.LogError("ERROR: TRYING TO REMOVE ITEM FROM EMPTY INVENTORY SLOT");
        }
    }
}
