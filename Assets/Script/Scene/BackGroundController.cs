using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] float _bgMovePercent = 0.25f;
    Transform _player;
    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _addPos = transform.position;
    }

    Vector3 _beforePos;
    Vector3 _addPos;
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        _beforePos = _beforePos - _player.position;
        _beforePos *= _bgMovePercent;
        _addPos += _beforePos;
        Vector3 move = new Vector3(_player.position.x + _addPos.x, _player.position.y + _addPos.y, pos.z);
        Debug.Log(_beforePos.x);
        gameObject.transform.position= move;
        _beforePos = _player.position;
    }
}
