using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetRunninng(bool running)
    {
        _animator.SetBool("running", running);
    }

    public void SetEquip()
    {
        _animator.SetTrigger("equip");
    }
    public void SetHolding(bool hold)
    {
        _animator.SetBool("holding", hold);
    }

    public void SetHold()
    {
        _animator.SetTrigger("hold");
    }

    public void SetThrow()
    {
        _animator.SetTrigger("throw");
    }
}
