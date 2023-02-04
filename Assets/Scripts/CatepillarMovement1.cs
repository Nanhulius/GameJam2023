using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class CatepillarMovement1 : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpForce = 500;
    [SerializeField] private float maxXForce = 500;
    [SerializeField] private ParticleSystem launchParticles;
    private float startJumpTimer;
    private float timedJumpForce;

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
            Debug.Log(rb.velocity.magnitude);
            startJumpTimer = Time.time;
            if (rb.velocity.magnitude == 0)
            {
                launchParticles.Play();
            }                     
             else
                return;
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            launchParticles.Stop();
            if (rb.velocity.magnitude > 0)
                return;
            else                 
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float relativePositionX = mousePosition.x - rb.position.x;

                //CalculateTimer(timedJumpForce);
                float timehold = (Time.time - startJumpTimer) + 1;

                if (timehold > 4.0f) {
                    timehold = 4.0f;
                }

                timedJumpForce = jumpForce * timehold;
                timedJumpForce = (timedJumpForce > maxJumpForce) ? maxJumpForce : timedJumpForce;
                float powerX = timehold * (relativePositionX / 3);

                if (mousePosition.x > rb.position.x)
                {
                    if (!facingRight)
                    {
                        facingRight = true;
                        spriteRenderer.flipX = false;
                    }

                    rb.AddForce(new Vector2(powerX, timedJumpForce) , ForceMode2D.Impulse);
                    Debug.Log("Jumping Right" + powerX);
                    Debug.Log("Jumping Up" + timedJumpForce);
                }
                else
                {
                    if (facingRight)
                    {
                        facingRight = false;
                    }

                    powerX = timehold * (relativePositionX / 3);
                    spriteRenderer.flipX = true;
                    rb.AddForce(new Vector2(powerX, timedJumpForce) , ForceMode2D.Impulse);
                    Debug.Log("Jumping Left" + powerX);
                    Debug.Log("Jumping Up" + timedJumpForce);
                }
            }           
        }
    }
}
