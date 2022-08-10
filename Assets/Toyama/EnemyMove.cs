using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject _player1 = default;
    [SerializeField] float _speed = 3f;

    void Start()
    {

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
}
