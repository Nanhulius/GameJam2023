using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatepillarMovement : MonoBehaviour
{
    [SerializeField] float launchForce = 500;
    [SerializeField] float maxDragDistance = 5;

    Vector2 startPosition;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = rb.position;
        rb.isKinematic = true;
    }

    void OnMouseDown()
    {
        spriteRenderer.color = new Color(1, 0, 1);
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = rb.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();

        rb.isKinematic = false;
        rb.AddForce(direction * launchForce);

        spriteRenderer.color = new Color(1, 1, 1);
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > startPosition.x)
            desiredPosition.x = startPosition.x;

        rb.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        rb.position = startPosition;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
}
