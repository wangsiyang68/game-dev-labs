using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [System.NonSerialized]
    public bool collided;
    // Start is called before the first frame update
    public Animator brickAnimator;
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with mario!");
        // stop the blinking animation
        brickAnimator.SetTrigger("isCollision");
        // set collided to true
        collided = true;
    }
}
