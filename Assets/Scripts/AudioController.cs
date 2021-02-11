using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip swingSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
    
    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlaySwingSound()
    {
        audioSource.PlayOneShot(swingSound);
    }
}
