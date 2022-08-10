using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float _health = 100.0f;
    public Action<float> OnHealthChanged;
    public Action OnDeath;

    public void ReceiveDamage(float amount)
    {
        _health = Mathf.Max(_health - amount, 0);
        if(_health > 0) OnHealthChanged(_health);
        else OnDeath();
    }
}
