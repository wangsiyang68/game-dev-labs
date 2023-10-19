using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovementW5 : MonoBehaviour
{
    public GameConstants gameConstants;
    float deathImpulse;
    float upSpeed;
    float maxSpeed;
    float speed;
    [System.NonSerialized]
    public bool alive = true;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeath;
    public AudioSource marioStomp;

    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private Transform gameCamera;
    
    // Unity Events //
    public UnityEvent gameOver;
    public UnityEvent<int> incrementScore;

    // Start is called before the first frame update

    void Start()
    {
        // Set constants
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;
        Debug.Log("speed: " + speed + " maxspeed: " + maxSpeed +" dI: " + deathImpulse + " uS: " + upSpeed);
        // Get mario Sprite Renderer
        marioSprite = GetComponent<SpriteRenderer>();
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
        // get camera coordinates
        gameCamera = Camera.main.transform;
        // subscribe to scene manager scene change
        // SceneManager.activeSceneChanged += SetStartingPosition;

        // TO BE REFACTORED TO SOGA
        // GameManagerW5.instance.powerupAffectsPlayer.AddListener(RequestPowerupEffect);
    }

    public void RequestPowerupEffect(IPowerup powerup)
    {
        //Debug.Log("mario Requesting powerup effect from " + powerup.gameObject.name);
        powerup.ApplyPowerup(this);
    }

    public void MakeSuperMario()
    {
        Debug.Log("making super mario...");
        //need to change the mario box collider as well, TODO
    }

    public void SetStartingPosition(Scene current, Scene next)
    {
        if (next.name == "World-1-2")
        {
            // change the position accordingly in your World-1-2 case
            this.transform.position = new Vector3(2.5f, 0.5f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        // toggle state
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

 
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("mario collision with " + col.gameObject.name);
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    private bool moving = false;
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }
    void Move(int value)
    {

            Vector2 movement = new Vector2(value, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                Debug.Log("adding force of: " + movement * speed);
                marioBody.AddForce(movement * speed);
            }
       
    }
    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
    private bool jumpedState = false;
    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }
    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        { 
            if (marioBody.velocity.y >= 0)
            {
                Debug.Log("Collided with goomba!");
                // play death animation
                marioAnimator.Play("mario-death");
                // marioAudio.PlayOneShot(marioDeath);
                marioDeath.PlayOneShot(marioDeath.clip);
                alive = false;
            } 
            else
            {
                // stompOnGoomba.Stomp();
                //gameManager.SetStompSfxPitch(marioBody.velocity.y * -1);
                Debug.Log("Stomped on " + other.gameObject.name);
                marioStomp.PlayOneShot(marioStomp.clip);
                //raise the Increase Score Game Event
                incrementScore.Invoke(1);
                //directly access the goomba because it must be provided for us to reach here
                other.GetComponent<EnemyMovementW5>().flatten();
            }
        }
    }
    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }
    void GameOverScene()
    {
        gameOver.Invoke();
    }
    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(3f, 0.5f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(16, 5, -1);
    }
}