using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {

        foreach (Transform block in gameObject.transform)
        {
            //reset Block Animators
            block.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("gameRestart");

            // reset bounciness of the blocks
            if (block.transform.GetChild(0).TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidBody))
            {
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
            }




            if (block.childCount > 2)
            {
                // GameRestart() on BasePowerUp
                if (block.transform.GetChild(2).gameObject.TryGetComponent<BasePowerup>(out BasePowerup powerup))
                {
                    powerup.GameRestart();

                    // reset Spawn on BasePowerUp
                    powerup.hasSpawned = false;
                }

            }
        }


    }

    void Awake()
    {
        // other instructions
        // subscribe to Game Restart event
        GameManagerWeek3.instance.gameRestart.AddListener(GameRestart);
    }

}
