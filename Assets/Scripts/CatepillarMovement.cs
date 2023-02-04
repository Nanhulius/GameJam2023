using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class CatepillarMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpForce = 500;
    private float startJumpTimer;

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
        spriteRenderer.color = new Color(255, 0, 1);
        Debug.Log("Spriterenderer color is " + spriteRenderer.color);
        Debug.Log("MOUSE DOWN");
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
                startJumpTimer = Time.time;
                
            }                     
             else
                return;
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            if (rb.velocity.magnitude > 0)
                return;
            else                 
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float timeHold = Time.time - startJumpTimer;
                if (mousePosition.x > rb.position.x)
                {
                    if (!facingRight)
                    {
                        facingRight = true;
                        spriteRenderer.flipX = false;
                    }
                    float timedJumpForce = jumpForce * timeHold;

                    rb.AddForce(new Vector3(1, mousePosition.y) * timedJumpForce);
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
        }
    }
}
