using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stamina : MonoBehaviour
{
    [Header("------------- Stamina System -------------")]
    [Header("Stamina Control")]
    [SerializeField] private float _maxStamina = 100f;
    public float _currentStamina { get; private set; }

    [SerializeField] private float _maxStaminaCD = 10f;
    private float _currentStaminaCD;

    [SerializeField] private float _staminaRegen = 10f;
    [SerializeField] private float _exhaustedRegen = 20f;

    private bool _isActive = true;
    public bool _isExhausted { get; private set; }

    [SerializeField] private float _rightCost, _leftCost;

    [Header("Stamina Bar")]
    [SerializeField] private Slider _staminaSlider;

    [SerializeField] private Color _staminaFullColor;
    [SerializeField] private Color _staminaEmptyColor;
    [SerializeField] public Image slidercolor;

    void Start()
    {
        _currentStamina = _maxStamina;
        _currentStaminaCD = _maxStaminaCD;

        _staminaSlider.maxValue = _maxStamina;
        _staminaSlider.minValue = 0;

        StartCoroutine(RegenStamina());
    }

    void Update()
    {
        UpdateStaminaBar();
    }

    public void UseStamina(float staminaUsed)
    {
        if (_isExhausted) return;

        if (!_isActive) _isActive = true;
        _currentStaminaCD = _maxStaminaCD;
        _currentStamina = Mathf.Max(_currentStamina - staminaUsed, 0);

        Debug.Log("STAMINA USED " + staminaUsed + _isExhausted + _isActive + _currentStamina);
    }
    IEnumerator RegenStamina()
    {
        while (true)
        {


            if (_currentStamina <= 0)
            {
                _currentStamina = 0;
                _isExhausted = true;
            }

            //if active: waiting to not be active
            if (_isActive)
            {
                _currentStaminaCD -= 0.01f;

                if (_currentStaminaCD <= 0)
                {
                    _isActive = false;
                    _currentStaminaCD = _maxStaminaCD;
                }
            }
            else
            {
                float regenRate = _isExhausted ? _exhaustedRegen : _staminaRegen;

                _currentStamina = Mathf.Min(regenRate * 0.01f + _currentStamina, _maxStamina);
                Debug.Log(_currentStamina);

                if (_currentStamina == _maxStamina)
                {
                    _isExhausted = false;
                }
            }

            yield return new WaitForSeconds(0.01f);
        }

    }
    void UpdateStaminaBar()
    {
        _staminaSlider.value = _currentStamina;
        slidercolor.color = Color.Lerp(_staminaEmptyColor, _staminaFullColor, _currentStamina / _maxStamina);
    }


}
