using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private List<AudioSource> _soundPool = new();
    [SerializeField] private int _maxPlayingSounds = 30;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < _maxPlayingSounds; i++)
        {
            AudioSource tempSound = gameObject.AddComponent<AudioSource>();
            tempSound.volume = 1;
            tempSound.Stop();
            tempSound.spatialBlend = 1;
            tempSound.rolloffMode = AudioRolloffMode.Linear;
            _soundPool.Add(tempSound);
        }
    }
    public void PlaySound(AudioClip sound, Vector3 pos, float volume)
    {
        PlaySoundCore(sound, pos, volume);
    }

    public void PlaySound(AudioClip sound, Vector3 pos, float volume, float delayTime)
    {
        StartCoroutine(DelayPlaySound(sound, pos, volume, delayTime));
    }

    private IEnumerator DelayPlaySound(AudioClip sound, Vector3 pos, float volume, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlaySoundCore(sound, pos, volume);
    }

    private void PlaySoundCore(AudioClip sound, Vector3 pos, float volume)
    {
        if (_soundPool.Count > 0)
        {
            foreach (AudioSource tempSound in _soundPool)
            {
                if (tempSound.isPlaying == false)
                {
                    tempSound.volume = volume;
                    tempSound.clip = sound;
                    tempSound.gameObject.transform.position = pos;
                    tempSound.Play();
                    break;
                }
            }
        }
    }
}

