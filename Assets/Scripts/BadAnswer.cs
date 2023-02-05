using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadAnswer : MonoBehaviour
{
    [SerializeField] ParticleSystem baloonParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(CheckCollision(collision));
    }

    IEnumerator CheckCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            baloonParticle.Play();
            yield return new WaitForSeconds(1);
            Debug.Log("Changing Scene...");
            Application.Quit(); 
        }
    }
}
