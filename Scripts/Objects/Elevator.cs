using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Attributes")]
    public float pushSpeed;

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
        if (collision.gameObject.CompareTag("Nail"))
        {
            myRigibody.AddForce(new Vector2(0, pushSpeed), ForceMode2D.Impulse);
        }
    }
}