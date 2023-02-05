using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("Blue")) {
            PlayerPrefs.SetString("team", "Blue");
        }

        if (coll.gameObject.name.Contains("Red")) {
            PlayerPrefs.SetString("team", "Blue");
        }

        if (coll.gameObject.name.Contains("Kill"))
        {
            this.GetComponent<PlayerData>().Damage(1);
        }

        if (coll.gameObject.name.Contains("Exit")) {
            StartCoroutine(Exit(coll));
        }
    }

    IEnumerator Exit(Collider2D coll)
    {
        string scene = coll.gameObject.GetComponent<LoadScene>().getSceneName();
        coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        coll.gameObject.GetComponent<LoadScene>().popBubble();
        yield return new WaitForSeconds(0.7f);
        this.GetComponent<PlayerData>().LoadScene(scene);
    }

}
