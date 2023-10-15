using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController : MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;

    public void ButtonClick()
    {
        gameRestart.Invoke();
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
