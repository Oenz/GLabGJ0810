using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swich : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] string _animStateName = "";

    private void OnTriggerEnter2D(Collider2D collision) //コライダーがトリガーだったら反応する
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        _anim.Play(_animStateName);//アニメーションを再生する
    }
}