using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] jumpSFX;

    public float waitForSFX = 0f;

    public bool sfxTimer = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayJumpingAudio()
    {
        if (!sfxTimer)
        {
            audioSource.clip = jumpSFX[Random.Range(0, jumpSFX.Length)];
            audioSource.Play();
            waitForSFX = Random.Range(5.5f, 10.0f);
            StartCoroutine(WaitForJumpSound(waitForSFX));
        }
        else
            return;      
    }

    private IEnumerator WaitForJumpSound(float waitForSFX)
    {
        sfxTimer = true; 
        yield return new WaitForSeconds(waitForSFX);
        sfxTimer = false;
        StopCoroutine(WaitForJumpSound(waitForSFX));
    }
}
