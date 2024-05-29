using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : BasicEnemy
{
    public override void CheckDistance()
    {
        if (Vector2.Distance(chaseTarget.position, transform.position) <= chaseRadius)
        {
            Vector2 vector = Vector2.MoveTowards(transform.position, chaseTarget.position, speed * Time.deltaTime);
            myRigidbody.MovePosition(vector);
        }
    }
}
