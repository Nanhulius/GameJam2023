using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEditorInternal;
using UnityEngine;

public class CatepillarMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 4;
    [SerializeField] private float maxJumpForce = 10;
    [SerializeField] private ParticleSystem launchParticlesLeft;
    [SerializeField] private ParticleSystem launchParticlesRight;
    [SerializeField] private ParticleSystem flyParticlesLeft;
    [SerializeField] private ParticleSystem flyParticlesRight;
    private float startJumpTimer;
    private float timedJumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator matoAnimator;

    private bool facingRight = true;
    private bool flying = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        matoAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.isKinematic = false;
        matoAnimator.Play("IdleMato");
    }

    private void OnMouseDown()
    {
        //spriteRenderer.color = new Color(255, 0, 1);
        //Debug.Log("Spriterenderer color is " + spriteRenderer.color);
    }


    void Update()
    {
        MoveCatepillar();
        CheckIdleMato();
    }

    private void MoveCatepillar()               // Moves the Player character
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (rb.velocity.magnitude == 0)
            {
                matoAnimator.SetBool("Jumping", true);
                startJumpTimer = Time.time;
                if (!facingRight)
                    launchParticlesRight.Play();
                else
                    launchParticlesLeft.Play();
            }                     
             else
                return;
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            matoAnimator.SetBool("Jumping", false);
            matoAnimator.SetBool("Flying", true);
            launchParticlesRight.Stop();
            launchParticlesLeft.Stop();
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
                    flyParticlesLeft.Play();
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
                    flyParticlesRight.Play();
                }
            }           
        }
        
    }

    private void CheckIdleMato()                    //Checks if Player character is in idle-mode for animator
    {
        if (rb.velocity.magnitude == 0)
        {
            matoAnimator.SetBool("Idle", true);
            matoAnimator.SetBool("Flying", false);
        }
    }
}
