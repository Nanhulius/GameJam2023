using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] int health = 1;
    [SerializeField] ParticleSystem deathParticle;

    IEnumerator GameOver()
    {
        Debug.Log("Game Over!");
        this.GetComponent<CatepillarMovement>().enabled = false;;
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
