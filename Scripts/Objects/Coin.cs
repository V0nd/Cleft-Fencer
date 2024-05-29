using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Data")]
    public FloatValue playerCoins;

    [Header("Attributes")]
    public float coinValue;
    private const float extraHeight = 0.1f;

    [Header("References")]
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider;
    public LayerMask platform;

    private bool started;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        started = true;
    }

    private void FixedUpdate()
    {
        if(started)
        {
            RandomDirectionRun();
            started = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StopAfterTouchingGround();  
    }

    void RandomDirectionRun()
    {
        myRigidbody.AddForce(new Vector2(Random.Range(-5, 5), Random.Range(1, 10)), ForceMode2D.Impulse);
    }

    void StopAfterTouchingGround()
    {
        if (boxCollider.IsTouchingLayers(platform) && !IsWalled())
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, extraHeight, platform);
        return raycastHit.collider != null;
    }

    private bool IsWallLefted()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, extraHeight, platform);
        return raycastHitLeft.collider != null;
    }

    private bool IsWallRighted()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, extraHeight, platform);
        return raycastHitRight.collider != null;
    }

    private bool IsWalled()
    {
        return IsWallRighted() || IsWallLefted();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerCoins.currentValue += coinValue;
            Destroy(this.gameObject);
        }
    }
}
