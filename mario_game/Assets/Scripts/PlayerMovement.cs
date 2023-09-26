using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    public float deathImpulse = 45;
    [System.NonSerialized]
    public bool alive = true;
    public TextMeshProUGUI scoreText;
    public GameObject restartButton;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameObject gameOverCanvas;
    public Animator marioAnimator;

    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    // Start is called before the first frame update
    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        gameOverCanvas.GetComponent<Canvas>().enabled = true;
        // scoreText.alpha = 0;
        // Hide score and restart button on the top left
        scoreText.gameObject.SetActive(false);
        restartButton.GetComponent<Button>().gameObject.SetActive(false);
    }
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.gameObject.CompareTag("Ground")) onGroundState = true;
        // if (col.gameObject.CompareTag("Ground") && !onGroundState)
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (alive)
        {
            {
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                // Vector2 movement = new Vector2(moveHorizontal, 0);
                // marioBody.AddForce(movement * speed);

                if (Mathf.Abs(moveHorizontal) > 0)
                {
                    Vector2 movement = new Vector2(moveHorizontal, 0);
                    // check if it doesn't go beyond maxSpeed
                    if (marioBody.velocity.magnitude < maxSpeed)
                        marioBody.AddForce(movement * speed);
                }

                // stop
                if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
                {
                    // stop
                    marioBody.velocity = Vector2.zero;
                }

                if (Input.GetKeyDown("space") && onGroundState)
                {
                    marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                    onGroundState = false;
                    // update animator state
                    marioAnimator.SetBool("onGround", onGroundState);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("mario-death");
            // marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-16.01f, -0.47f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        scoreText.gameObject.SetActive(true);
        // reset button
        restartButton.GetComponent<Button>().gameObject.SetActive(true);
        // reset game over canvas
        gameOverCanvas.GetComponent<Canvas>().enabled = false;
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        // reset score
        jumpOverGoomba.score = 0;
        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
    }
}