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
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 direction = transform.position - other.transform.position;
            bool isForceUp = Vector2.Dot(direction.normalized, Vector2.up) > 0.25f;
            Debug.Log("dot pdt is " + Vector2.Dot(direction.normalized, Vector2.up));
            if (isForceUp)
            {
                Debug.Log("Brick Collided with mario!");
                // stop the blinking animation
                brickAnimator.SetTrigger("isCollision");
                // set collided to true
                collided = true;
            }
        }
    }
}
