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

            // GameRestart() on BasePowerUp
            if (block.transform.GetChild(2).gameObject.TryGetComponent<BasePowerup>(out BasePowerup powerup))
            {
                if (powerup.powerupType == PowerupType.MagicMushroom)
                {
                    powerup.GameRestart();
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
