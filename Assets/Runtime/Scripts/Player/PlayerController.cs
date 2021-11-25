using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private float gravity = 1.8f * 9.8f;
    [SerializeField] private float flapForce = 10;
    [SerializeField] private float flapAngleDegress = 20;
    [SerializeField] private float rotateDownSpeed = 5;
    private Vector3 velocity;
    public Vector3 Velocity => velocity;

    private float zRot;


    private PlayerInputs input;


    private void Awake()
    {
        input = GetComponent<PlayerInputs>();
    }
    private void Update()
    {
        ProcessInputs();
        ProcessMovement();
        ProcessRotation();

        transform.rotation = Quaternion.Euler(0, 0, zRot);
        transform.position += velocity * Time.deltaTime;


    }

    private float ProcessInputs()
    {
        if (input.ScreenTap())
        {
            velocity.y = flapForce;
            zRot = flapAngleDegress;
        }
        return zRot;
    }
    private void ProcessMovement()
    {
        velocity.x = playerSpeed;
        velocity.y -= gravity * Time.deltaTime;
    }

    private void ProcessRotation()
    {
        if (velocity.y < 0)
        {
            zRot -= rotateDownSpeed * Time.deltaTime;
            zRot = Mathf.Max(-90, zRot);
        }
    }




}
