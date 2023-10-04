using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer spriteRenderer;
    private bool flattenState; 

    public Vector3 startPosition;
    public Sprite stompedGoomba;
    void Start()
    {
        startPosition = transform.localPosition;
        Debug.Log("start " + transform.name + startPosition.x + " " + startPosition.y + " " + startPosition.z);
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void GameRestart()
    {
        Debug.Log("restart " + transform.name + startPosition.x + " " + startPosition.y + " " + startPosition.z);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }


    void Update()
    {
        if (!flattenState)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
    public void flatten()
    {
        flattenState = true;
        GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderer.sprite = stompedGoomba;
        // Destroy after 1s
        Destroy(gameObject, 0.5f);
    }
}