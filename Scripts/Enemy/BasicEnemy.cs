using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;
    private float originalValueOfSpeed;
    private readonly float idleSpeed = 0f;
    public float chaseRadius;
    private float currentHealth;

    [Header("Player")]
    public FloatValue playerCoins;

    [Header("Coins")]
    public GameObject coin;
    public Transform coinSpawnPoint;

    [Header("References")]
    public Enemy enemy;
    public Transform chaseTarget;
    public LayerMask playerLayerMask;
    [HideInInspector]public Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentHealth = enemy.maxHealth;
        originalValueOfSpeed = speed;
    }

    private void Update()
    {
        IdleWhenPlayerAbove();
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if(Vector2.Distance(chaseTarget.position, transform.position) <= chaseRadius)
        {
            if(transform.position.x > chaseTarget.position.x)
            {
                myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
            }
            else
            {
                myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
            }
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemy.CoinDrop(coin, coinSpawnPoint);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(enemy.attackStrength);

            var player = collision.GetComponent<PlayerController>();
            player.knockbackCount = player.knockBackTime;

            if (collision.transform.position.x < transform.position.x)
            {
                player.knockbackRight = true;
            }
            else
            {
                player.knockbackRight = false;
            }
        }
    }

    private void IdleWhenPlayerAbove()
    {
        if(IsPlayerAbove())
        {
            speed = idleSpeed;
        }
        else
        {
            speed = originalValueOfSpeed;
        }
    }

    private bool IsPlayerAbove()
    {
        float extraHeight = 4f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, extraHeight, playerLayerMask);
        return raycastHit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}