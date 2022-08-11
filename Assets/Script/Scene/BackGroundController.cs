using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] float _bgMovePercentX = 0.25f;
    [SerializeField] float _bgMovePercentY = 0.05f;
    [SerializeField] float _bgSizeX = 50;
    [SerializeField] SpriteRenderer _sp1;
    [SerializeField] SpriteRenderer _sp2;
    Transform _player;
    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _addPos = transform.position;
        //_addPos.y *= -1;
    }

    Vector3 _beforePos;
    [SerializeField] Vector3 _addPos;
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        _beforePos = _beforePos - _player.position;
        _beforePos.x *= _bgMovePercentX;
        _beforePos.y *= _bgMovePercentY;
        _addPos += _beforePos;
        Vector3 move = new Vector3(_addPos.x, _addPos.y, pos.z);
        //Debug.Log(_beforePos.x);
        gameObject.transform.position = move;
        _beforePos = _player.position;


        //_player.position.x
    }
}
