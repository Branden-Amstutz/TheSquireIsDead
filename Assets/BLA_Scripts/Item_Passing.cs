using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Passing : MonoBehaviour
{
    private ItemStateControl _stateController;
    private Rigidbody _rb;
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stateController = GetComponent<ItemStateControl>();
    }

}
