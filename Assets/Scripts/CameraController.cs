using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameConstants gameConstants;
    Vector3 cameraStartingPosition;
    private Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // cameraStartingPosition = gameConstants.cameraStartingPosition;
        // this.transform.position = cameraStartingPosition;

        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - cameraStartingPosition.x);

        offset = cameraStartingPosition.x - player.position.x;
        startX = cameraStartingPosition.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;

        cameraStartingPosition = transform.position;
    }

    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
    }

    public void GameRestart()
    {
        // reset camera position
        this.transform.position = cameraStartingPosition;

    }

    void Awake()
    {
        // other instructions
        // subscribe to Game Restart event
        // GameManagerWeek3.instance.gameRestart.AddListener(GameRestart);
    }
}
