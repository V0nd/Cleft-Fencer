using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryHealthBearer : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;
    public float waitTime;
    private bool invincible;

    private readonly System.Random rnd = new System.Random();
    private readonly int[] runDirections = new int[2] { -1, 1 };
    private int directionIndex;

    [Header("Referneces")]
    public PlayerData playerData;
    public LayerMask platform;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider;

    void Start()
    {
        directionIndex = rnd.Next(0, runDirections.Length);
        InvincibleAfterSpawn();
        //InvicibleAfterSpawnCo(waitTime);
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        if(IsGrounded())
        {
            myRigidbody.velocity = new Vector2(speed * runDirections[directionIndex], 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Nail") && !invincible)
        {
            playerData.temporaryHealth.currentValue += 1;
            this.gameObject.SetActive(false);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, extraHeight, platform);
        return raycastHit.collider != null;
    }

    private void InvincibleAfterSpawn()
    {
        invincible = true;
        Invoke("TurnOffInvincible", waitTime);
    }

    private void TurnOffInvincible()
    {
        invincible = false;
    }

    private IEnumerator InvicibleAfterSpawnCo(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}