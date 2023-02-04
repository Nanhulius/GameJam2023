using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] jumpSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayJumpingAudio()
    {
        audioSource.clip = jumpSFX[Random.Range(0, jumpSFX.Length)];
        audioSource.Play();
    }
}
