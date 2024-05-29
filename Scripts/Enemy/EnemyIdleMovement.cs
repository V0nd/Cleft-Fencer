using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;

    [Header("References")]
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider;
    public LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float extraHeight = 5f;

        if(RightSideGrounded(extraHeight) && LeftSideGrounded(extraHeight))
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        }
        else if(!RightSideGrounded(extraHeight) || !LeftSideGrounded(extraHeight))// || WallLefted() || WallRighted())
        {
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
            speed *= -1;
        }

    }

    private bool RightSideGrounded(float extraHeight)
    {
        RaycastHit2D raycastRight = Physics2D.Raycast(boxCollider.bounds.max, Vector2.down, boxCollider.bounds.extents.y + extraHeight, platformLayerMask);
        Debug.DrawRay(boxCollider.bounds.max, Vector2.down * (boxCollider.bounds.extents.y + extraHeight), Color.yellow);

        return raycastRight.collider;
    }

    private bool LeftSideGrounded(float extraHeight)
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(boxCollider.bounds.min, Vector2.down, boxCollider.bounds.extents.y + extraHeight, platformLayerMask);
        Debug.DrawRay(boxCollider.bounds.min, Vector2.down * (boxCollider.bounds.extents.y - extraHeight), Color.yellow);

        return raycastLeft.collider;
    }

    private bool WallRighted()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, platformLayerMask);
        return raycastHitRight.collider != null;
    }

    private bool WallLefted()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, platformLayerMask);
        return raycastHitRight.collider != null;
    }
}
