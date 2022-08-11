using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject _player1 = default;
    [SerializeField] float _speed = 3f;

    private Animator _anim;
    private Player _check;
    bool _plt = false;

    void Start()
    {
        _check = FindObjectOfType<Player>();
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_plt == false)
        {
            _anim.SetBool("move", false);
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && _check._enemyCheck == false)
        {
            _plt = true;
            _anim.SetBool("move",true);
            Vector3 targeting = (_player1.transform.position - transform.position).normalized;//�v���C���[-�G�L�����̈ʒu�֌W����������擾���A���x����艻
            GetComponent<Rigidbody>().velocity = new Vector3(targeting.x * _speed, 0);//�v���C���[�ǂ�
            if (targeting.x > 0)
            {
                transform.rotation = new Quaternion(0, 1f, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            _plt = false;
        }
    }

}

