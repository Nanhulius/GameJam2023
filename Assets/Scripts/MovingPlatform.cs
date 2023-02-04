using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float TimeInterval;
    private float currentAmount = 0;
    private float x = 0;
    private float y = 0;
    private bool goingForward = true;
    [SerializeField] int amount = 5;
    [SerializeField] bool axisY = false;
    [SerializeField] bool axisX = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

        
    void FixedUpdate()
    {
        TimeInterval += Time.deltaTime;
        if (TimeInterval >= 0.1)
        {
            TimeInterval = 0;
            x = axisX ? 0.1f : 0;
            y = axisY ? 0.1f : 0;

            if (currentAmount <= 0) {
                goingForward = true;
            } else if (currentAmount >= amount ) {
                goingForward = false;
            }

            if (goingForward) {
                currentAmount += 0.1f;
                rb2D.MovePosition(new Vector2(rb2D.position.x + x, rb2D.position.y + y));
            } else  {
                currentAmount -= 0.1f;
                rb2D.MovePosition(new Vector2(rb2D.position.x - x, rb2D.position.y - y));
            }
        }
    }
}
