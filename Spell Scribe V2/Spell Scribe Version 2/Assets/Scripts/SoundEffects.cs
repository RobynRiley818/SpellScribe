using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip playerHurt;
    public AudioClip dewHurt;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "hurt":
                audioSource.clip = playerHurt;
                break;
            case "dewHurt":
                audioSource.clip = dewHurt;
                break;
        }

        audioSource.Play();
    }
}
