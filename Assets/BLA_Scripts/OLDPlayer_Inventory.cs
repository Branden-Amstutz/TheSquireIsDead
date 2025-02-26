using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDPlayer_Inventory : MonoBehaviour
{
    private Camera _cam;

    public Transform RightHand { get; private set; }
    public bool IsRightOccupied { get; private set; }
    public Transform LeftHand { get; private set; }
    public bool IsLeftOccupied { get; private set; }

    [SerializeField] private Vector3 _rightHandLocation, _leftHandLocation;

    void Start()
    {
        IsRightOccupied = false;
        IsLeftOccupied = false;
        _cam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            OccupyInventory(ThisGuy, true);
        }*/
        if (Input.GetMouseButton(1))
        {
            RemoveInventory(true);
            RemoveInventory(false);
        }
    }

    //Inventory Scripts
    public void OccupyInventory(Transform other, bool isRight)
    {
        other.SetParent(_cam.transform);

        if (isRight)
        {
            RightHand = other;
            RightHand.GetComponent<Rigidbody>().isKinematic = true;
            RightHand.localPosition = _rightHandLocation;
            RightHand.localRotation = Quaternion.identity;

            IsRightOccupied = true;
        }
        else
        {
            LeftHand = other;
            LeftHand.GetComponent<Rigidbody>().isKinematic = true;
            LeftHand.localPosition = _leftHandLocation;
            LeftHand.localRotation = Quaternion.identity;

            IsLeftOccupied = true;
        }

    }
    public void RemoveInventory(bool isRight)
    {
        if (isRight)
        {
            if (IsRightOccupied)
            {
                RightHand.SetParent(null);

                RightHand.GetComponent<Rigidbody>().isKinematic = false;
                RightHand.GetComponent<Rigidbody>().AddForce(_cam.transform.forward * 5, ForceMode.Impulse);

                RightHand = null;

                IsRightOccupied = false;
            }
        }
        else
        {
            if (IsLeftOccupied)
            {
                LeftHand.SetParent(null);

                LeftHand.GetComponent<Rigidbody>().isKinematic = false;
                LeftHand.GetComponent<Rigidbody>().AddForce(_cam.transform.forward * 5, ForceMode.Impulse);

                LeftHand = null;

                IsLeftOccupied = false;
            }

        }
    }
}
