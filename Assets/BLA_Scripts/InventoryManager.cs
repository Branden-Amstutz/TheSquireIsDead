using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public void PickupItem(ItemStateControl item, InventorySlot inventorySlot)
    {
        if(inventorySlot.State != InventorySlot.InventoryState.empty)
        {
            Debug.LogError("ERROR: INVENTORY OCCUPIED");
            return;
        }
        if(item.State != ItemStateControl.ItemState.floor)
        {
            Debug.LogError("ERROR: ITEM NOT ON GROUND");
        }

        item.SetHand(inventorySlot.transform);
        inventorySlot.AddItem(item.transform);

    }

    public void DropItem(ItemStateControl item, InventorySlot inventorySlot)
    {
        if (inventorySlot.State != InventorySlot.InventoryState.ocuppied)
        {
            Debug.LogError("ERROR: TRYING TO REMOVE ITEM FROM EMPTY SLOT");
            return;
        }

        item.SetFloor();
        inventorySlot.RemoveItem();
    }


    public IEnumerator PassItem(InventorySlot passer, InventorySlot reciever)
    {
        if(passer.State != InventorySlot.InventoryState.ocuppied)
        {
            Debug.LogError("ERROR: PASSER INVENTORY EMPTY");
            yield break;
        }
        if(reciever.State != InventorySlot.InventoryState.empty)
        {
            Debug.LogError("ERROR: RECIEVER INVENTORY EMPTY");
            yield break;
        }

        ItemStateControl item = passer.EquippedItem.GetComponent<ItemStateControl>();
        
        passer.RemoveItem();

        item.SetPassing();

        yield return StartCoroutine(PassingCoroutine(passer.transform ,reciever.transform));

        PickupItem(item, reciever);

    }
    // math for tragectory of pass
    private IEnumerator PassingCoroutine(Transform item, Transform target)
    {
        Vector3 targetPos = target.position;
        Vector3 startPos = item.transform.position;
        Vector3 controlPoint = (startPos + targetPos) / 2 + Vector3.up * 2.0f;

        float timeToTarget = Vector3.Distance(startPos, targetPos) / 10;
        float elsapseTime = 0;

        while (elsapseTime < timeToTarget)
        {
            elsapseTime += Time.deltaTime;
            float t = elsapseTime / timeToTarget;

            targetPos = target.position;

            item.transform.position = CalculateParabola(startPos, controlPoint, targetPos, t);

            yield return null;
        }

    }
    // Quadratic Bezier curve formula
    private Vector3 CalculateParabola(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return (1 - t) * (1 - t) * start + 2 * (1 - t) * t * control + t * t * end;
    }



}
