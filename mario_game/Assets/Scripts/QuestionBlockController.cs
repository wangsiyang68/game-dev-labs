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
    public GameObject powerupPrefab;

    public GameObject powerup;
    // Start is called before the first frame update
    void Start()
    {
        // play blinking block animation
        questionBlockSprite = GetComponent<SpriteRenderer>();
        questionBlockSpring = GetComponent<SpringJoint2D>();
        questionBlockBody = GetComponent<Rigidbody2D>();
        collided = false;
    }

    //void GameRestart()
    //{
    //    if (powerup == null)
    //    {
    //        // instantiate the selected prefab
    //        GameObject powerup = Instantiate(powerupPrefab);
    //        // remap the variables in the inspector
    //        powerup = powerup;
    //    }
    //}

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
                Debug.Log("QB Collided by Mario from the bottom!");  
                // change the sprite to a brown block
                questionBlockSprite.sprite = block_brown;
                // stop the blinking animation
                questionBlockAnimator.SetTrigger("spawned");
                // set collided to true
                collided = true;
            }
        }
    }


}
