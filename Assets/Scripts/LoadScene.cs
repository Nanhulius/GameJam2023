using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName = "menu";
    [SerializeField] ParticleSystem baloonParticle;

    public string getSceneName() {
        return sceneName;
    }

    public void popBubble() {
        this.GetComponent<SpriteRenderer>().enabled = false;
        baloonParticle.Play();
    }
}
