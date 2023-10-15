using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator activatePowerup(BasePowerup powerup)
    {
        // turn on collider and set dynamic 
        powerup.GetComponent<BoxCollider2D>().enabled = true;
        powerup.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // typically contain a yield return statement
        yield return new WaitForSeconds(0.1f);        
        // spawn the powerup
        powerupAnimator.SetTrigger("spawned");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            Debug.Log("QBMP collided with player");
            Vector2 direction = transform.position - other.transform.position;
            bool isForceUp = Vector2.Dot(direction.normalized, Vector2.up) > 0.25f;
            Debug.Log("dot pdt is " + Vector2.Dot(direction.normalized, Vector2.up));
            if (isForceUp)
            {
                // activate the powerup components
                StartCoroutine(activatePowerup(powerup));
                this.GetComponent<Animator>().SetTrigger("spawned");
            }
        }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }



}