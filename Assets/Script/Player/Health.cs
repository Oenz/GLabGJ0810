using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float _health = 100.0f;
    [SerializeField] float _maxHealth = 100f;
    public Action<float> OnHealthChanged;
    public Action OnDeath;
    [SerializeField] float _perfect = 1;
    [SerializeField] Slider _slider;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void ReceiveDamage(float amount)
    {
        _perfect = 1 - _maxHealth / _health;
        if(_slider != null)
        {
            _slider.value = amount;
        }

        _health = Mathf.Max(_health - amount, 0);
        if(_health > 0) OnHealthChanged(_health);
        else OnDeath();
    }

    private void Update()
    {
        ReceiveDamage(1f * Time.deltaTime);
    }
}
