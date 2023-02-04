using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("Kill"))
        {
            this.GetComponent<PlayerData>().Damage(1);
        }

        if (coll.gameObject.name.Contains("Exit")) {
            string scene = coll.gameObject.GetComponent<LoadScene>().getSceneName();

            this.GetComponent<PlayerData>().LoadScene(scene);
        }
    }

}
