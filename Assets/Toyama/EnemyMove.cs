using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject _player1 = default;
    [SerializeField] float _speed = 3f;

    private Player _check;

    void Start()
    {
        _check = FindObjectOfType<Player>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && _check._enemyCheck == false)
        {
            Debug.Log("ugoiteru");
            Vector3 targeting = (_player1.transform.position - transform.position).normalized;//プレイヤー-敵キャラの位置関係から方向を取得し、速度を一定化
            GetComponent<Rigidbody>().velocity = new Vector3(targeting.x * _speed, 0);//プレイヤー追う
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
