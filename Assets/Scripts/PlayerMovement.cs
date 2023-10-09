using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float maxspeed = 20;

    public float upSpeed = 10;

    public float deathImpulse;

    [System.NonSerialized]
    public bool onGroundState = true;
    private Rigidbody2D marioBody;

    // global variables
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public GameObject enemies;
    public GameObject blocks;


    // for animation
    public Animator marioAnimator;

    // for audio
    public AudioSource marioAudio;

    public AudioSource marioDeathAudio;

    // state
    [System.NonSerialized]
    public bool alive = true;

    public Transform gameCamera;



    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        // gameOverScreen.SetActive(false);
        marioSprite = GetComponent<SpriteRenderer>();
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();


        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

        // // subscribe to scene manager scene change
        // SceneManager.activeSceneChanged += SetStartingPosition;

    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (alive)
        {
            if (value == -1 && faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
                if (marioBody.velocity.x > 0.05f)
                    marioAnimator.SetTrigger("onSkid");

            }

            else if (value == 1 && !faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
                if (marioBody.velocity.x < -0.05f)
                    marioAnimator.SetTrigger("onSkid");
            }
        }
    }




    private bool moving = false;

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxspeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    private bool jumpedState = false;

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 40, ForceMode2D.Force);
            jumpedState = false;

        }
    }


    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            // Debug.Log("on ground");
            return true;
        }
        else
        {
            // Debug.Log("not on ground");
            return false;
        }
    }

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 8);
    void OnCollisionEnter2D(Collision2D col)
    {

        if (((collisionLayerMask & (1 << col.GetContact(0).collider.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = onGroundCheck();
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // This is for the mario-die animation
    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    // This is for the mario-die animation
    void GameOver()
    {
        GameManagerWeek3.instance.GameOver();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (alive)
        {
            // check if Goomba, and whether Mario is hitting goomba from above
            if (other.gameObject.CompareTag("Stomp") && (transform.position.y > other.gameObject.transform.position.y))
            {
                GameObject parent = other.gameObject.transform.parent.gameObject;
                parent.GetComponent<Animator>().SetTrigger("stomped");
                // Debug.Log("STOMP");
                other.enabled = false;
                parent.GetComponent<EdgeCollider2D>().enabled = false;
                GameManagerWeek3.instance.IncreaseScore(1);
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                // Debug.Log("Collided with goomba!");
                // play death animation
                marioAnimator.Play("mario_die");
                marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
                alive = false;
            }
        }
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(0.0f, -5.4f, 0.0f);
        // reset camera position
        // gameCamera.position = new Vector3(0, -2, -10);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // //reset Blocks
        // foreach (Transform block in blocks.transform)
        // {
        //     if (block.transform.gameObject.TryGetComponent<BlockCoinController>(out BlockCoinController coinController))
        //     {
        //         coinController.ResetBlock();
        //     }
        // }

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;



    }


    // public void SetStartingPosition(Scene current, Scene next)
    // {
    //     if (next.name == "World1-2")
    //     {
    //         // change the position accordingly in your World-1-2 case
    //         this.transform.position = new Vector3(0.0f, -5.4f, 0.0f);
    //     }
    // }

    void Awake()
    {
        // other instructions
        // subscribe to Game Restart event
        GameManagerWeek3.instance.gameRestart.AddListener(GameRestart);
    }


}