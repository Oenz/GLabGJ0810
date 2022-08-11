using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recovery : Item
{

    [SerializeField] int _recoverLife = 10;

    public override void Activate()
    {
        //FindObjectOfType<>().AddLife(_recoverLife);
    }
}

