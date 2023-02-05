using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] int health = 1;
    [SerializeField] ParticleSystem deathParticle;
    //[SerializeField] SpriteRenderer hat;
    public string team = "none";

    public void Awake() {
        team = PlayerPrefs.GetString("team");

        if (team == "Red") {
            //hat.enabled = true;
            this.GetComponent<SpriteRenderer>().color = new Color(255,0,0,1); 
        } 
        if (team == "Blue") {
            //hat.enabled = true;
            this.GetComponent<SpriteRenderer>().color = new Color(0,0,255,1); 
        }
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over!");
        this.GetComponent<CatepillarMovement>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        deathParticle.Play();
        yield return new WaitForSeconds(2);
        deathParticle.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Damage(int amount = 0) {
        health -= amount;
        if (health <= 1) {
           StartCoroutine(GameOver());
        }
    }

    public int getHealth () {
        return health;
    }

    public void Heal(int amount = 0) {
        if (health + amount <= maxHealth) {
            health += amount;
        } else {
            health = maxHealth;
        }
    }

    public void LoadScene(string name = "") {
        SceneManager.LoadScene(name);
    }
}
