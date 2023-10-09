using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoinController : MonoBehaviour
{

    public PlayerMovement mario;

    public Animator blockAnimator;

    public GameObject currentBlock;

    public Rigidbody2D childBody;

    // for audio
    public AudioSource coinAudio;

    private float coinDelay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        mario = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Mario")
        {

            if (!blockAnimator.GetBool("alrHit"))
            {
                blockAnimator.SetTrigger("onHit");
                StartCoroutine(waitCoin());
            }
        }
    }

    public void ResetBlock()
    {
        blockAnimator.SetBool("alrHit", false);
        // if (currentBlock.CompareTag("QuestionBox"))
        // {
        childBody.bodyType = RigidbodyType2D.Dynamic;
        // }
        blockAnimator.SetTrigger("gameRestart");


    }

    IEnumerator waitCoin()
    {
        blockAnimator.SetBool("alrHit", true);
        // wait for coinDelay
        yield return new WaitForSeconds(coinDelay);
        // Disable the movement of QuestionBox
        if (currentBlock.CompareTag("QuestionBox"))
        {
            childBody.bodyType = RigidbodyType2D.Static;
        }

        // Play Sound
        coinAudio.volume = UnityEngine.Random.Range(0.8f, 1.0f);
        coinAudio.pitch = UnityEngine.Random.Range(0.85f, 1.1f);
        coinAudio.PlayOneShot(coinAudio.clip);


    }
}
