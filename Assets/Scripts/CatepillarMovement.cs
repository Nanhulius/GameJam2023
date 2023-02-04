using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class CatepillarMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator matoAnimator;

    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        matoAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.isKinematic = false;
        matoAnimator.Play("MatoAnimation");
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = new Color(1, 0, 1);
    }


    void Update()
    {
        MoveCatepillar();
    }

    private void MoveCatepillar()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (rb.velocity.magnitude == 0)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("Mouseposition" + mousePosition.x);
                Debug.Log("Playerposition" + rb.position.x);
                if (mousePosition.x > rb.position.x)
                {
                    if (!facingRight)
                    {
                        facingRight = true;
                        spriteRenderer.flipX = false;
                    }
                    rb.AddForce(new Vector3(1, mousePosition.y) * jumpForce);
                    Debug.Log("Jumping Right");
                }
                else
                {
                    if (facingRight)
                    {
                        facingRight = false;

                    }
                    spriteRenderer.flipX = true;
                    rb.AddForce(new Vector3(-1, mousePosition.y) * jumpForce);
                    Debug.Log("Jumping Left");
                }
            }                     
             else
                return;
        }
    }
}
