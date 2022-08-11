using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerIndex = 1;
    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _maxSpeed = 500.0f;
    [SerializeField] float _jumpPower = 2.0f;
    [SerializeField] float _groundHeight = 0.5f;
    [SerializeField] float _interactReach = 20;

    public bool _enemyCheck = false;
    Rigidbody _rb;
    CapsuleCollider _capsule;
    float _horizontal;
    Ability _ability;
    SpriteRenderer _sr;
    PlayerAnimController _pac;

    bool _isRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsule = GetComponent<CapsuleCollider>();
        _ability = GetComponent<Ability>();
        _sr = GetComponent<SpriteRenderer>();
        _pac = GetComponent<PlayerAnimController>();

        var _health = GetComponent<Health>();
        _health.OnHealthChanged += HealthChange;
        _health.OnDeath += Death;
    }

    public bool IsLookRight()
    {
        _pac.SetRunninng(_horizontal != 0);
        if (_horizontal != 0)
        {
            _isRight = _horizontal >= 0;
        }
        _sr.flipX = !_isRight;
        return _isRight;
    }

    void HealthChange(float hp)
    {

    }

    void Death()
    {

    }

    void Update()
    {
        _horizontal = Input.GetAxis($"P{_playerIndex}Horizontal");
        if (_ability.IsActive()) _horizontal = 0;
        IsLookRight();

        if (Input.GetButtonDown($"P{_playerIndex}Jump"))
        {
           CheckIsGround();
        }

        if (Input.GetButtonDown($"P{_playerIndex}Interact"))
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
        dir *= _interactReach;
        RaycastHit hit;
        Debug.DrawRay(s, dir, Color.red, 2f);
        if (Physics.Raycast(s, dir, out hit))
        {
            hit.collider.gameObject.GetComponent<IInteract>()?.Interact();
            Object obj = hit.collider.gameObject.GetComponent<Object>();

            if(obj != null)
            {
                _ability.EquipObject(hit.collider.gameObject);
                _enemyCheck = hit.collider.gameObject.CompareTag("Enemy");
            }
                    
            
            Debug.Log(hit.transform.position);
            Debug.DrawLine(hit.transform.position, hit.transform.position + Vector3.up, Color.green, 5);
        }
    }
}
