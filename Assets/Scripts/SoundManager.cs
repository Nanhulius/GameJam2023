using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] bubbleBurstSFX;
    public AudioClip[] jumpSFX;
    public AudioClip chargeJumpSFX;
    public AudioClip deathSFX;
    public AudioClip skullDeathSFX;
    public AudioClip breakWallSFX;
    private float waitForSFX = 0f;

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

    public void PlayChargeJumpAudio()
    {
        if (!sfxTimer)
        {
            audioSource.clip = chargeJumpSFX;
            audioSource.Play();
            waitForSFX = Random.Range(5.5f, 10.0f);
            StartCoroutine(WaitForJumpSound(waitForSFX));
        }
        else
            return;
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathSFX;
        audioSource.Play();
    }

    public void PlaySkullDeathAudio()
    {
        audioSource.clip = skullDeathSFX;
        audioSource.Play();
    }

    

    public void PlayBubbleBurstAudio()
    {
        audioSource.clip = bubbleBurstSFX[Random.Range(0, bubbleBurstSFX.Length)];
        audioSource.Play();
    }

    private IEnumerator WaitForJumpSound(float waitForSFX)
    {
        sfxTimer = true; 
        yield return new WaitForSeconds(waitForSFX);
        sfxTimer = false;
        StopCoroutine(WaitForJumpSound(waitForSFX));
    }
}
