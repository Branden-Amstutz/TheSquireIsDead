using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Targetter : MonoBehaviour
{
    [SerializeField] private Player_InventoryManager _manager;

    private Camera _cam;

    [SerializeField] private GameObject _pickupText;
    [SerializeField] private GameObject _passText;

    [SerializeField] private float _pickupDistance = 5.0f;
    [SerializeField] private float _passDistance = 10.0f;

    [SerializeField] private float _pickupTime = 5.0f;
    [SerializeField] private float _passTime = 1.0f;
    private float _currentButtonHold;

    enum KeyPressed
    {
        main,
        off,
        none
    }

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        
    }

    void LookForTarget()
    {
        RaycastHit hit;
        Ray ray = new(_cam.transform.position, _cam.transform.forward);

        if(Physics.Raycast(ray, out hit, 50))
        {
            float distance = Vector3.Distance(this.transform.position, hit.transform.position);

            if (distance > _passDistance) return;

            if (hit.transform.tag == "Object" && distance <= _pickupDistance)
            {
                _passText.SetActive(false);
                _pickupText.SetActive(true);

                _pickupText.transform.position = Vector3.Lerp(hit.point, _cam.transform.position, 0.1f);

                if(ClickPrimaryInteract)
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

    bool ClickPrimaryInteract(float targetTime)
    {
        while (Input.GetKeyDown(KeyCode.Q))
        {
            _currentButtonHold += Time.deltaTime;

            if(_currentButtonHold >= targetTime)
            {
                return true;
            }
        }
        _currentButtonHold = 0;
        return false;
    }
}
