using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        soundManager.PlayBadEndingAudio();
    }
}
