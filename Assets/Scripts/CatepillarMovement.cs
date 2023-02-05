using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.PackageManager.Requests;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.VFX;

public class CatepillarMovement : MonoBehaviour
{
    private SoundManager soundManager;
    [SerializeField] private float jumpForce = 4;
    [SerializeField] private float maxJumpForce = 10;
    [SerializeField] private ParticleSystem launchParticlesLeft;
    [SerializeField] private ParticleSystem launchParticlesRight;
    [SerializeField] private ParticleSystem flyParticlesLeft;
    [SerializeField] private ParticleSystem flyParticlesRight;


    private float startJumpTimer;
    private float timedJumpForce;

    private bool facingRight = true;
    private bool canJump = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator matoAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        matoAnimator = GetComponent<Animator>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        matoAnimator.SetBool("Idle", true);
        matoAnimator.SetBool("Flying", false);
        canJump = true;
    }

    void OnCollisionExit2D(Collision2D coll) {
        canJump = false;
    }

    private void MoveCatepillar()               // D'oh, moves the Player character
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (rb.velocity.magnitude == 0 || canJump)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePosition.x > rb.position.x) {
                    facingRight = true;
                    spriteRenderer.flipX = false;
                } else {
                    facingRight = false;
                    spriteRenderer.flipX = true;
                }
                
                soundManager.PlayChargeJumpAudio();
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
            if (rb.velocity.magnitude > 0 && !canJump)
                return;
            else                 
            {

            matoAnimator.SetBool("Jumping", false);
            matoAnimator.SetBool("Flying", true);

            soundManager.PlayJumpingAudio();

            launchParticlesRight.Stop();
            launchParticlesLeft.Stop();
                
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
