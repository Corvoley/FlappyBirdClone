using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovementTrigger : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StopAllMoviment();
        }
    }
}
