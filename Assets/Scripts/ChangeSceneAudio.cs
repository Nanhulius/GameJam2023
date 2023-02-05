using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip changeSceneSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayChangeSceneAudio()
    {
        audioSource.clip = changeSceneSFX;
        audioSource.Play();
    }
}
