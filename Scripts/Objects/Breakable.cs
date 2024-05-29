using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int hitsToSurvive;
    private int hitsSurvived = 0;

    public int numberOfHatchlings;
    public GameObject hatchling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nail"))
        {
            hitsSurvived++;
            SpawnHatchling();
            Hit();
        }
    }

    public void Hit()
    {
        if (hitsSurvived >= hitsToSurvive)
        {
            this.gameObject.SetActive(false);
        }
    }

    void SpawnHatchling()
    {
        if(hatchling != null)
        {
            Instantiate(hatchling, transform.position, transform.rotation);
        }
    }
}
