using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Animator goombaAnimator;

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
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
            child.GetComponent<EdgeCollider2D>().enabled = true;

            // need to change implementation if its a different hierarchy than Goomba
            child.GetChild(0).GetComponentInChildren<EdgeCollider2D>().enabled = true;
            goombaAnimator.SetTrigger("gameRestart");

        }
    }
}
