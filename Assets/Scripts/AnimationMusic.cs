using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMusic : MonoBehaviour
{
    [SerializeField] AudioClip keyboardType;
    AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void KeyboardSoundPlay()
    {
        aSource.clip = keyboardType;
        aSource.Play();
    }



}
