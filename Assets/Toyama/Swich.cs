using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swich : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] string _animStateName = "";

    private void OnTriggerEnter2D(Collider2D collision) //�R���C�_�[���g���K�[�������甽������
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        _anim.Play(_animStateName);//�A�j���[�V�������Đ�����
    }
}