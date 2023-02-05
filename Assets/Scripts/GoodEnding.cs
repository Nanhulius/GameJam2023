using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        soundManager.PlayGoodEndingAudio();
    }
}
