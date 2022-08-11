using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Health health = GetComponent<Health>();
        health.OnHealthChanged += HeathChaged;
        health.OnDeath += Death;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HeathChaged(float hp)
    {

    }
    void Death()
    {
        Destroy(gameObject);
    }
}
