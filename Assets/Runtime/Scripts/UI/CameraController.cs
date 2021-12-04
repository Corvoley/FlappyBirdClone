using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController player;


    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        currentPosition.x = playerPosition.x;

        transform.position = currentPosition;
    }
}
