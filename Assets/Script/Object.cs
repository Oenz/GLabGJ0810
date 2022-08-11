using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    bool _isAttack = false;
    [SerializeField] float _damage = 100f;

    public void SetAttack()
    {
        _isAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isAttack) return;
        _isAttack = false;
        if (collision.gameObject.CompareTag("Player")) return;
        collision.gameObject.GetComponent<Health>()?.ReceiveDamage(_damage);
    }

}
