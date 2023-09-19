using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoinController : MonoBehaviour
{

    public PlayerMovement mario;

    public Animator blockAnimator;

    public GameObject currentBlock;

    public Rigidbody2D childBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Mario" && !mario.onGroundState)
        {
            // Debug.Log("Hit While Jumping");
            blockAnimator.SetTrigger("onHit");

            // Disable the movement of QuestionBox
            if (currentBlock.CompareTag("QuestionBox"))
            {
                childBody.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    public void ResetBlock()
    {
        blockAnimator.SetTrigger("gameRestart");
        if (currentBlock.CompareTag("QuestionBox"))
        {
            childBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
