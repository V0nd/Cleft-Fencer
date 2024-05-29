using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [Header("Push")]
    public float pushSpeedX;
    public float pushSpeedY;

    private Rigidbody2D myRigibody;

    private void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Nail"))
        {
            if(collision.transform.position.x > transform.position.x)
            {
                myRigibody.AddForce(new Vector2(-pushSpeedX, pushSpeedY), ForceMode2D.Impulse);
            }
            else if(collision.transform.position.x < transform.position.x)
            {
                myRigibody.AddForce(new Vector2(pushSpeedX, pushSpeedY), ForceMode2D.Impulse);
            }
        }
    }

}
