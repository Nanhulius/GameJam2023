using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    
    [SerializeField] int health = 2;
    [SerializeField] Sprite broken;
    [SerializeField] Sprite normal;

    public int maxHealth = 2;
    public SpriteRenderer spriteRenderer;

    void Awake() {
        maxHealth = health;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name.Contains("Player"))
        {
            health--;

            if (health <= maxHealth / 2) {
                spriteRenderer.sprite = broken;
            }

            if (health < 0) {
                this.gameObject.SetActive(false);
            }
        }
    }
}
