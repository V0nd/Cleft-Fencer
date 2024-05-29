using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float speed;
    public int attack;
    public float attackRange;
    public float projectileLifetime;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    void Fly()
    {
        myRigidbody.velocity = speed * transform.right;
        Destroy(this.gameObject, projectileLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
        {
            collision.GetComponent<BasicEnemy>().GetDamage(attack);
        }
        Destroy(this.gameObject, projectileLifetime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, attackRange);
    }
}
