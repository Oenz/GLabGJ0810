using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    GameObject _player;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _throwPower = 5;
    [SerializeField] float _interpSpeed = 4;

    float _interpPer = 1;

    GameObject _current;
    Rigidbody _currentRigidbody;

    private void Start()
    {
        _player = FindObjectOfType<Player>().gameObject;
    }

    public void EquipObject(GameObject obj)
    {
        _current = obj;
        _interpPer = 0;
        _startPos = _current.transform.position;
        _currentRigidbody =_current.GetComponent<Rigidbody>();
        _currentRigidbody.useGravity = false;
        
    }

    Vector3 _startPos;

    private void Update()
    {
        if (_current == null) return;
        //Mathf.Lerp

        Vector3 targetPos = _player.transform.position + _offset;

        if (_interpPer <= 1)
        {
            _current.transform.position = Vector3.Lerp(_startPos , targetPos, _interpPer);
            _interpPer += 1 / _interpSpeed * Time.deltaTime;
        }
        else
        {
            _current.transform.position = targetPos;
        }

        _current.transform.Rotate(Vector3.up + Vector3.right * 0.1f) ;
    }

    public bool ThrowObject(Vector3 targetPos)
    {
        if (_current == null || _interpPer <= 1) return false;

        Vector3 _dir = targetPos - _current.transform.position;
        _currentRigidbody.useGravity = true;
        _currentRigidbody.AddForce(_dir * _throwPower, ForceMode.Impulse);
        _current = null;
        _currentRigidbody = null;
        return true;
    }
}
