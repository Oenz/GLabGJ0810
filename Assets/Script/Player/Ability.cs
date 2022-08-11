using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    Player _player;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _throwPower = 5;
    [SerializeField] float _interpSpeed = 4;
    [SerializeField] float _gaugeScale = 2;
    [SerializeField] int _level = 0;
    [SerializeField] float _gauge = 0;

    float _maxGauge = 0;

    float _interpPer = 1.1f;

    GameObject _current;
    Rigidbody _currentRigidbody;
    PlayerAnimController _pac;
    float _currentMass = 0;
    Vector3 _startPos;


    private void Start()
    {
        _player = GetComponent<Player>();
        _pac = GetComponent<PlayerAnimController>();
        LevelUp(1);
    }

    public void LevelUp(int amount)
    {
        _level += amount;
        _maxGauge = _level * _gaugeScale;
    }

    public bool IsActive()
    {
        return _current != null;
    }

    public void EquipObject(GameObject obj)
    {
        if (_current != null || _interpPer <= 1) return;
        _current = obj;
        _interpPer = 0;
        _startPos = _current.transform.position;
        _currentRigidbody = _current.GetComponent<Rigidbody>();
        _currentRigidbody.useGravity = false;
        _currentMass = _currentRigidbody.mass;
        _current.layer = 11;
        _pac.SetEquip();
    }



    private void Update()
    {
        _pac.SetHolding(IsActive());
        if (_current == null)
        {
            if (_maxGauge > _gauge)
            {
                _gauge += _maxGauge * Time.deltaTime;
            }
            return;
        }
        _gauge -= _currentMass * Time.deltaTime;

        if (_gauge <= 0)
        {
            if (IsActive())
            {
                ClearObject();
            }
            return;
        }

        Vector3 curOffset = _offset;
        curOffset.x *= _player.IsLookRight() ? 1 : -1;

        Vector3 targetPos = _player.transform.position + curOffset;

        if (_interpPer <= 1)
        {
            _current.transform.position = Vector3.Lerp(_startPos, targetPos, _interpPer);
            _interpPer += 1 / _interpSpeed * Time.deltaTime;
        }
        else
        {
            _interpPer = 5;
            if (IsActive() && _interpPer != 5)
            {
                _pac.SetHold();
            }
            _current.transform.position = targetPos;

            
        }
        Vector3 dir = Vector3.forward;
        if (!_player._enemyCheck) dir += Vector3.right;
       _current.transform.Rotate(dir * 0.5f);
    }

    public bool ThrowObject(Vector3 targetPos)
    {
        if (_current == null || _interpPer <= 1) return false;

        Vector3 _dir = targetPos - _current.transform.position;
        _currentRigidbody.AddForce(_dir * _throwPower, ForceMode.Impulse);
        _current.GetComponent<Object>().SetAttack();
        _pac.SetThrow();
        ClearObject();
        return true;
    }

    void ClearObject()
    {

        _current.layer = 0;
        _currentRigidbody.useGravity = true;
        _current = null;
        _currentRigidbody = null;
    }
}
