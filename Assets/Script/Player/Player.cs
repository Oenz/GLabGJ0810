using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _maxSpeed = 500.0f;
    [SerializeField] float _jumpPower = 2.0f;
    [SerializeField] float _groundHeight = 0.5f;

    public bool _enemyCheck = false;
    Rigidbody _rb;
    CapsuleCollider _capsule;
    float _horizontal;
    Ability _ability;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsule = GetComponent<CapsuleCollider>();
        _ability = GetComponent<Ability>();

        var _health = GetComponent<Health>();
        _health.OnHealthChanged += HealthChange;
        _health.OnDeath += Death;
    }

    void HealthChange(float hp)
    {

    }

    void Death()
    {

    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
           CheckIsGround();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interact();
        }
    }

    public void CheckIsGround()
    {
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        Debug.DrawRay(rayPosition, Vector3.down * _groundHeight, Color.red, 2f);
        if (Physics.Raycast(ray, _groundHeight))
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }
    
    private void FixedUpdate()
    {
        Vector3 dir = Vector3.right * _horizontal * _speed;
        dir.y = _rb.velocity.y;
        if (dir.x > _maxSpeed) dir = new Vector3(_maxSpeed, dir.y, dir.z);
        if (dir.x < -_maxSpeed) dir = new Vector3(-_maxSpeed, dir.y, dir.z);
        if (dir.z > _maxSpeed) dir = new Vector3(dir.x, dir.y, _maxSpeed);
        if (dir.z < -_maxSpeed) dir = new Vector3(dir.x, dir.y, -_maxSpeed);
        _rb.velocity = dir;
    }

    void Interact()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10.0f;
        Vector3 t = Camera.main.ScreenToWorldPoint(pos);
        t.z = 0;

        if (_ability.ThrowObject(t)) return;

        Vector3 s = transform.position;// + transform.forward * 2;
        s.z = 0;
       // Debug.DrawLine(t, s, Color.red, 2f);
        Vector3 dir =  t - s;
        dir *= 1000;
        RaycastHit hit;
        Debug.DrawRay(s, dir, Color.red, 2f);
        if (Physics.Raycast(s, dir, out hit))
        {
            hit.collider.gameObject.GetComponent<IInteract>()?.Interact();
            if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Object"))
            {
                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    _enemyCheck = true;

                }
                    _ability.EquipObject(hit.collider.gameObject);
            }
            Debug.Log(hit.transform.position);
            Debug.DrawLine(hit.transform.position, hit.transform.position + Vector3.up, Color.green, 5);
        }
    }
}
