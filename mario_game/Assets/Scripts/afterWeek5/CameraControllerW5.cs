using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerW5 : MonoBehaviour
{

    public Transform endLimit; // GameObject that indicates end of map
    private Transform player; // Mario's Transform
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;

        // define the starting position based on the inspector
        startPosition = this.transform.position; //will it change after camera moves?
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        else if (desiredX > endX)
            this.transform.position = new Vector3(endX, this.transform.position.y, this.transform.position.z);
        else
            this.transform.position = new Vector3(startX, this.transform.position.y, this.transform.position.z);
    }
    public void GameRestart()
    {
        // reset camera position
        //Vector3 startPosition = new Vector3(16, 5, -1); //should it be hardcoded?
        Debug.Log("CameraController Resetting Position...");
        transform.position = startPosition;
    }
}
