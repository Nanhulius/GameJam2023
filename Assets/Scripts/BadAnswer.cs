using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadAnswer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision", collision.gameObject);
        CheckCollision(collision);
    }

    private void CheckCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Changing Scene...");
            Application.Quit(); 
        }
    }
}
