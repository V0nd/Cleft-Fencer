using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform brother;
    private bool canTeleport = false;
    private Collider2D player;

    private void Start()
    {
        
    }

    private void Update()
    {
        Teleportation(canTeleport);
    }

    private void Teleportation(bool canTeleport)
    {
        if (canTeleport && Input.GetButtonDown("Equip"))
            player.GetComponent<Transform>().transform.position = new Vector2(brother.position.x, brother.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canTeleport = true;
            player = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }
}
