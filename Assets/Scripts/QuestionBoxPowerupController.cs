using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            // show disabled sprite
            this.GetComponent<Animator>().SetTrigger("spawned");

            // activate box collider on mushroom
            if (powerup.powerupType == PowerupType.MagicMushroom)
            {
                this.transform.parent.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
            }

            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");



            //deactivate edge collider that prevents powerup from dropping out of the box
            this.transform.parent.GetChild(2).GetComponent<EdgeCollider2D>().enabled = false;
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
}