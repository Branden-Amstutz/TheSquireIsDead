using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Stamina))]
public class Player_PickupPass : MonoBehaviour
{
    private OLDPlayer_Inventory _inventory;
    private Player_Stamina _stamina;
    private Camera _cam;

    [SerializeField] private GameObject _pickupText;
    [SerializeField] private GameObject _passText;

    [SerializeField] private float _pickupDistance = 5.0f;
    [SerializeField] private float _passDistance = 10.0f;

    private void Start()
    {
        _inventory = this.gameObject.GetComponent<OLDPlayer_Inventory>();
        _stamina = this.gameObject.GetComponent<Player_Stamina>();
        _cam = this.gameObject.GetComponentInChildren<Camera>();

    }
    private void Update()
    {
        LookForTarget();
    }

    void LookForTarget()
    {
        RaycastHit hit;
        Ray ray = new (_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out hit, 50))
        {
            if (hit.collider != null)
            {
                float distance = Vector3.Distance(this.transform.position, hit.transform.position);

                if (distance > _passDistance) return;

                if (hit.transform.tag == "Object" && distance <= _pickupDistance)
                {
                    _passText.SetActive(false);
                    _pickupText.SetActive(true);

                    _pickupText.transform.position = Vector3.Lerp(hit.point, _cam.transform.position, 0.1f);

                    HandlePickup(hit.transform);
                }
                else if (hit.transform.tag == "Player" && distance <= _passDistance)
                {
                    _pickupText.SetActive(false);
                    _passText.SetActive(true);

                    _passText.transform.position = Vector3.Lerp(hit.point, _cam.transform.position, 0.1f);

                    HandlePass(hit.transform, hit.transform.InverseTransformPoint(hit.point));
                }
                else
                {
                    _passText.SetActive(false);
                    _pickupText.SetActive(false);

                }

            }

        }
        else
        {
            _passText.SetActive(false);
            _pickupText.SetActive(false);
        }

        Debug.DrawRay(_cam.transform.position, _cam.transform.TransformDirection(Vector3.forward) * 10, Color.yellow);
    }

    void HandlePickup(Transform objectToPickup)
    {
        if (!_inventory.IsRightOccupied && Input.GetKeyDown(KeyCode.E))
        {
            _inventory.OccupyInventory(objectToPickup, true);
        }
        else if (!_inventory.IsLeftOccupied && Input.GetKeyDown(KeyCode.Q))
        {
            _inventory.OccupyInventory(objectToPickup, false);
        }
    }

    void HandlePass(Transform targetPlayer, Vector3 hitLocation)
    {
        OLDPlayer_Inventory otherInventory = targetPlayer.GetComponent<OLDPlayer_Inventory>();
        bool isRightSide = hitLocation.x >= 0;

        if ((isRightSide && otherInventory.IsRightOccupied) || (!isRightSide && otherInventory.IsLeftOccupied))
            return;

        if (Input.GetKeyDown(KeyCode.E) && _inventory.IsRightOccupied)
        {
            //StartCoroutine(TransferObject(otherInventory, true, isRightSide));
        }
        else if (Input.GetKeyDown(KeyCode.Q) && _inventory.IsLeftOccupied)
        {
            //StartCoroutine(TransferObject(otherInventory, false, isRightSide));
        }
    }

    /*IEnumerator TransferObject(OLDPlayer_Inventory otherInventory, bool fromRightHand, bool toRightHand)
    {
        Transform objectToPass = fromRightHand ? _inventory.RightHand : _inventory.LeftHand;

        _inventory.RemoveInventory(objectToPass == _inventory.RightHand);

        //yield return StartCoroutine(objectToPass.GetComponent<Item_Passing>().PassItem(otherInventory.transform, 0));
        
        otherInventory.OccupyInventory(objectToPass, toRightHand);
    }*/

}
