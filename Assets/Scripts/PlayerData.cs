using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver() {
        Debug.Log("Game Over!");
    }

    public void Damage(int amount = 0) {
        health -= amount;
        if (health <= 1) {
            this.GameOver();
        }
    }

    public void Heal(int amount = 0) {
        if (health + amount <= maxHealth) {
            health += amount;
        } else {
            health = maxHealth;
        }
    }
}
