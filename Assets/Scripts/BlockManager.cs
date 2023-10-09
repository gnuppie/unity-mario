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
        //reset Blocks
        foreach (Transform block in gameObject.transform)
        {
            if (block.transform.gameObject.TryGetComponent<BlockCoinController>(out BlockCoinController coinController))
            {
                coinController.ResetBlock();
            }
        }
    }
}
