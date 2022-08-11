using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    bool _isAttack = false;
    [SerializeField] public int requireLevel = 1;
    [SerializeField] float _damage = 100f;
    [SerializeField] bool _isTekkyuu = false;
    [SerializeField] float _ttekkyuuRanage = 100f;
    [SerializeField] GameObject _particle;

    public void SetAttack()
    {
        _isAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isAttack) return;
        _isAttack = false;
        if (_isTekkyuu)
        {
            RaycastHit[] a = Physics.SphereCastAll(transform.position, _ttekkyuuRanage, Vector3.forward);
            foreach (RaycastHit hit in a)
            {
                if (hit.collider.gameObject.CompareTag("Player")) continue;
                hit.collider.gameObject.GetComponent<Health>()?.ReceiveDamage(_damage);
            }
            Destroy(Instantiate(_particle, gameObject.transform.position + Vector3.back * 15, _particle.transform.rotation), 0.5f);

        }
        if (collision.gameObject.CompareTag("Player")) return;
        Debug.Log(collision.gameObject.name);
        collision.gameObject.GetComponent<Health>()?.ReceiveDamage(_damage);
        if(gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<Health>()?.ReceiveDamage(_damage);
        }
        Debug.Log($"{collision.gameObject.GetComponent<Health>() != null}");
    }

}
