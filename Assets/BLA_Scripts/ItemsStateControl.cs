using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemStateControl : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _col;

    public enum ItemState
    {
        mainHand,
        offHand,
        passing,
        floor
    }
    public ItemState State { get; private set; }

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        _col = this.GetComponent<Collider>();
    }

    public void SetHand(Transform inventorySlot)
    {
        _rb.isKinematic = true;
        _col.isTrigger = true;

        this.transform.SetParent(inventorySlot);
        this.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        State = inventorySlot.GetComponent<InventorySlot>().isMainHand ? ItemState.mainHand : ItemState.offHand;
    }
    public void SetPassing()
    {
        this.transform.SetParent(null);

        State = ItemState.passing;
    }
    public void SetFloor()
    {
        _rb.isKinematic = false;
        _col.isTrigger = false;

        this.transform.SetParent(null);

        State = ItemState.floor;
    }


}
