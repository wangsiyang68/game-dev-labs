using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicMushroomPowerupW5 : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
    }
    public UnityEvent<IPowerup> PowerupCollected;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player
            // PowerupManager.instance.PowerupCollected.Invoke(this);
            PowerupCollected.Invoke(this);
            // ApplyPowerup(col.gameObject.GetComponent<PlayerMovementW5>());
            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
        Debug.Log("i push the shroom");
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
        PlayerMovementW5 pm = (PlayerMovementW5) i;
        pm.MakeSuperMario();
    }
}