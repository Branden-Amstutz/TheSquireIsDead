using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player_Stamina))]
[RequireComponent(typeof(Rigidbody))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody _rb;

    private Player_Stamina _stamina;
    
    [Header("------------- Movement System -------------")]

    [Header("Variables walk/run")]
    [SerializeField] private float _exhaustedSpeed = 1.0f;
    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private float _sprintSpeed = 5.0f;
    [SerializeField] private float _horizontalInput, _verticalInput;

    [SerializeField] private float _sprintCost = 5.0f;

    [Header("Variables Jump")]
    [SerializeField] private float _jumpPower = 5.0f;
    private bool _isGrounded;

    [SerializeField] private float _jumpCost = 15.0f;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _stamina = this.GetComponent<Player_Stamina>();
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.forward * _verticalInput + transform.right * _horizontalInput;
        direction.Normalize();

        float moveSpeed = _walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && _stamina._currentStamina > 0 && !_stamina._isExhausted)
        {
            moveSpeed = _sprintSpeed;
            _stamina.UseStamina(_sprintCost * Time.deltaTime);
        }
        else if (_stamina._isExhausted)
        {
            moveSpeed = _exhaustedSpeed;
        }

        _rb.velocity = new Vector3(direction.x * moveSpeed, _rb.velocity.y, direction.z * moveSpeed);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_stamina._isExhausted)
        {
            _stamina.UseStamina(_jumpCost);
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGrounded = false;
        }
    }
}