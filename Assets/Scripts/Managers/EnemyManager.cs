using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
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
        foreach (Transform child in GameObject.Find("Enemies").transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
            child.GetComponent<EdgeCollider2D>().enabled = true;

            // need to change implementation if its a different hierarchy than Goomba
            child.GetChild(0).GetComponent<EdgeCollider2D>().enabled = true;
            child.GetComponent<Animator>().SetTrigger("gameRestart");

        }
    }

    void Awake()
    {
        // other instructions
        // subscribe to Game Restart event
        GameManagerWeek3.instance.gameRestart.AddListener(GameRestart);
    }
}
