using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObj : MonoBehaviour,IInteract
{
    [SerializeField] float _x = 0f;
    [SerializeField] float _y = 0f;
    [SerializeField] float _z = 0f;
    [SerializeField]
    private bool activate = false;//�v���C���[����I������Ă邩�ۂ��t���O
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;//�v���C���[���G���Ă��Ȃ���Ԃ̎��͕������Z������Ȃ��悤�ɂ���
    }
    // Update is called once per frame
    void Update()
    {
        //�v���C���[����I��������ԂɂȂ邩�̊Ď�
        if (activate)
        {
            Interact();
        }
    }

    public void Interact()
    {
        //�U��q�̓�������������Ƃ�iskinematic����
        rb.isKinematic = false;
        rb.AddForce(_x,_y, _z,ForceMode.Impulse);
        activate = false;
    }
}
