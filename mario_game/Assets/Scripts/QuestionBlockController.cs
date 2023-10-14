using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlockController : MonoBehaviour
{
    public Animator questionBlockAnimator;
    public Sprite block_brown;
    private SpriteRenderer questionBlockSprite;
    private SpringJoint2D questionBlockSpring;
    private Rigidbody2D questionBlockBody;
    [System.NonSerialized]
    public bool collided;
    // Start is called before the first frame update
    void Start()
    {
        // play blinking block animation
        questionBlockSprite = GetComponent<SpriteRenderer>();
        questionBlockSpring = GetComponent<SpringJoint2D>();
        questionBlockBody = GetComponent<Rigidbody2D>();
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (questionBlockBody.velocity.y == 0 && collided)
        {
            // set the rigidbody to static when the block has stopped moving after it is hit
            questionBlockBody.bodyType = RigidbodyType2D.Static;
            questionBlockSpring.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("QB Collided with mario!");
        // change the sprite to a brown block
        questionBlockSprite.sprite = block_brown;
        // stop the blinking animation
        questionBlockAnimator.enabled = false;
        // set collided to true
        collided = true;
    }


}
