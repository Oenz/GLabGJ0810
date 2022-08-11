using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : MonoBehaviour
{

    [SerializeField] AudioClip _sound = default;
    [SerializeField] ActivateTiming _whenActivated = ActivateTiming.Get;

    public abstract void Activate();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
        }
    }

    enum ActivateTiming
    {
        /// <summary>Žæ‚Á‚½Žž‚É‚·‚®Žg‚¤</summary>
        Get,
    }
}
