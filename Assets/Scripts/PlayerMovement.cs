using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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

    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public GameObject blocks;

    public JumpOverGoomba jumpOverGoomba;


    public GameObject gameOverScreen;
    public GameObject restartBtn;


    // for animation
    public Animator marioAnimator;

    // for audio
    public AudioSource marioAudio;

    public AudioClip marioDeath;

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
        gameOverScreen.SetActive(false);
        marioSprite = GetComponent<SpriteRenderer>();
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();


        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

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
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
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


    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        gameOverScreen.SetActive(true);
        scoreText.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        restartBtn.transform.localPosition = new Vector3(0.0f, -100.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            // Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("mario_die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
            // GameOverScene();
        }
    }

    public void RestartButtonCallback(int input)
    {
        // Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(0.0f, -5.4f, 0.0f);
        // reset camera position
        gameCamera.position = new Vector3(0, -2, -10);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        //reset Blocks
        foreach (Transform block in blocks.transform)
        {
            if (block.transform.gameObject.TryGetComponent<BlockCoinController>(out BlockCoinController coinController))
            {
                coinController.ResetBlock();
            }
        }


        // reset score
        jumpOverGoomba.score = 0;

        // reset from gameover screen
        gameOverScreen.SetActive(false);
        scoreText.transform.localPosition = new Vector3(-663.0f, 472.0f, 0.0f);
        restartBtn.transform.localPosition = new Vector3(899.0f, 485.0f, 0.0f);

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;



    }


}