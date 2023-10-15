using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private float maxOffset;
    private float enemyPatroltime;
    private bool alive = true;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private Vector3 localStartPosition;
    private Vector3 startPosition;
    public AudioSource stompAudio;
    public UnityEvent damagePlayer;
    public UnityEvent<int> increaseScore;

    void Awake()
    {
        localStartPosition = this.transform.localPosition;
    }

    void Start()
    {
        maxOffset = gameConstants.goombaMaxOffset;
        enemyPatroltime = gameConstants.goombaPatrolTime;
        // startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (alive)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
    }


    public void GameRestart()
    {
        Debug.Log("Goomba Restarted");
        this.gameObject.SetActive(true);
        this.GetComponent<Collider2D>().enabled = true;

        // if we do this, need to check if dead or not, otherwise if it was alive, we have extra trigger
        // if (!alive) this.GetComponent<Animator>().SetTrigger("restartGame");

        alive = true;
        transform.localPosition = localStartPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

    public void SetInactive()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayDeathSound()
    {
        this.GetComponent<Collider2D>().enabled = false;
        alive = false;
        stompAudio.PlayOneShot(stompAudio.clip);
    }






}