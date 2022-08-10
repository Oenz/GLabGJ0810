using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _maxSpeed = 500.0f;
    [SerializeField] float _jumpPower = 2.0f;

    Rigidbody _rb;
    float _horizontal;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(_rb.transform.up * _jumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = gameObject.transform.forward * _horizontal * _speed;
        dir.y = _rb.velocity.y;
        if (dir.x > _maxSpeed) dir = new Vector3(_maxSpeed, dir.y, dir.z);
        _rb.velocity = dir;
    }

    void Interact()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10.0f;
        Vector3 t = Camera.main.ScreenToWorldPoint(pos);
        t.x = 0;

        Vector3 s = transform.position + transform.forward * 2;
        s.x = 0;
       // Debug.DrawLine(t, s, Color.red, 2f);
        Vector3 dir =  t - s;
        dir *= 1000;
        RaycastHit hit;
        Debug.DrawRay(s, dir, Color.red, 2f);
        if (Physics.Raycast(s, dir, out hit))
        {
            hit.collider.gameObject.GetComponent<IInteract>()?.Interact();
            Debug.Log(hit.transform.position);
            Debug.DrawLine(hit.transform.position, hit.transform.position + Vector3.up, Color.green, 5);
        }
    }
}
