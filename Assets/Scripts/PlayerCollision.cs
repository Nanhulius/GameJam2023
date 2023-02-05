using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private SoundManager soundManager;
    private ChangeSceneAudio changeSceneAudio;
    private int health = 1;

    private void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        changeSceneAudio = GameObject.FindWithTag("ChangeScene").GetComponent<ChangeSceneAudio>();
        health = this.GetComponent<PlayerData>().getHealth();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (health <= 0) {
            return;
        }

        if (coll.gameObject.name.Contains("Blue")) {
            PlayerPrefs.SetString("team", "Blue");
        }

        if (coll.gameObject.name.Contains("Red")) {
            PlayerPrefs.SetString("team", "Red");
        }

        if (coll.gameObject.name.Contains("Skull"))
        {
            soundManager.PlaySkullDeathAudio();
            this.GetComponent<PlayerData>().Damage(1);
        }

        if (coll.gameObject.name.Contains("Kill"))
        {
        
            soundManager.PlayDeathSound();

            this.GetComponent<PlayerData>().Damage(1);
        }

        if (coll.gameObject.name.Contains("Exit")) {
            soundManager.PlayBubbleBurstAudio();
            changeSceneAudio.PlayChangeSceneAudio();
            StartCoroutine(Exit(coll));
        }
    }

    IEnumerator Exit(Collider2D coll)
    {
        string scene = coll.gameObject.GetComponent<LoadScene>().getSceneName();
        coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        coll.gameObject.GetComponent<LoadScene>().popBubble();
        yield return new WaitForSeconds(2.7f);
        this.GetComponent<PlayerData>().LoadScene(scene);
    }

}