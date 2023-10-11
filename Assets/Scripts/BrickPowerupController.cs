using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;

    public BasePowerup powerup;

    public bool isBreakable = false;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!powerup.hasSpawned)
            {
                // enable sprite
                this.GetComponent<SpriteRenderer>().enabled = true;


                if (!isBreakable) // IF IT IS NOT BREAKABLE, IT WILL TURN INTO DISABLED BLOCK
                {
                    this.GetComponent<Animator>().SetTrigger("spawned");

                }
                else // if Is Breakable, stays same state (Brick)
                {
                    //bounce
                }

                // activate box collider on anything besides a coin
                if (powerup.powerupType != PowerupType.Coin)
                {
                    this.transform.parent.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;

                    //deactivate edge collider that prevents powerup from dropping out of the box
                    this.transform.parent.GetChild(2).GetComponent<EdgeCollider2D>().enabled = false;
                }

                // spawn powerup
                powerupAnimator.SetTrigger("spawned");

            }
            else
            {
                // else if it's the breakable brick type
                // CURRENTLY: can stays as a bouncable brick although powerup has spawned

            }
        }
    }



    // used by animator
    public void Disable()
    {
        StartCoroutine(waitAnimation());
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private IEnumerator waitAnimation()
    {
        yield return new WaitForSeconds(0.2f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
