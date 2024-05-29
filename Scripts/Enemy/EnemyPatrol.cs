using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private bool mustPatrol;
    private bool mustFlip;
    private Rigidbody2D myRigidbody;
    public Transform groundCheckPosition;
    public LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPosition.position, 0.2f, platformLayerMask);
        }
    }

    void Patrol()
    {
        if(mustFlip)
        {
            Flip();
        }

        myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
    }
}
