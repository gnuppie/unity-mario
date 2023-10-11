
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 7 || col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        playSound();
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    public override void GameRestart()
    {
        base.GameRestart();
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<EdgeCollider2D>().enabled = true;
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }

    public void playSound()
    {
        AudioSource powerUpAudio = this.transform.GetComponent<AudioSource>();
        powerUpAudio.volume = UnityEngine.Random.Range(0.8f, 1.0f);
        powerUpAudio.pitch = UnityEngine.Random.Range(0.85f, 1.1f);
        powerUpAudio.PlayOneShot(powerUpAudio.clip);
    }
}