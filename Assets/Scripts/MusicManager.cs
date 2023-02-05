using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] musicList;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitForMusic());
    }

    private void PlayMusic()
    {
        audioSource.clip = musicList[Random.Range(0, musicList.Length)];
        audioSource.Play();
    }

    private IEnumerator WaitForMusic()
    {
        yield return new WaitForSeconds(2.0f);
        PlayMusic();
    }
}
