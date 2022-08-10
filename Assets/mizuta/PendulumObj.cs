using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObj : MonoBehaviour,IInteract
{
    [SerializeField] float _x = 0f;
    [SerializeField] float _y = 0f;
    [SerializeField] float _z = 0f;
    [SerializeField]
    private bool activate = false;//プレイヤーから選択されてるか否かフラグ
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;//プレイヤーが触っていない状態の時は物理演算がされないようにする
    }
    // Update is called once per frame
    void Update()
    {
        //プレイヤーから選択される状態になるかの監視
        if (activate)
        {
            Interact();
        }
    }

    public void Interact()
    {
        //振り子の動きが発生するときiskinematic解除
        rb.isKinematic = false;
        rb.AddForce(_x,_y, _z,ForceMode.Impulse);
        activate = false;
    }
}
