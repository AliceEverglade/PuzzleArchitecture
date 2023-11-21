using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class SFXHandler : MonoBehaviour
{
    private SFXHandler() { }

    private static SFXHandler _instance;
    private static readonly object _lock = new object();

    public static SFXHandler Instance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SFXHandler();
                }
            }
        }

        return _instance;
    }

    public void PlaySound(GameObject audioObject, AudioClip clip)
    {
        bool soundExists = GameObject.FindGameObjectWithTag("SFXPlayer") != null;
        Debug.Log(soundExists);

        if (soundExists)
        {
            Destroy(GameObject.FindGameObjectWithTag("SFXPlayer"));
        }

        audioObject.GetComponent<AudioSource>().clip = clip;
        GameObject spawnedSound = Instantiate(audioObject);
        spawnedSound.transform.parent = GameObject.FindGameObjectWithTag("SoundHandler").transform;
        Destroy(spawnedSound, 1);
    }
}
