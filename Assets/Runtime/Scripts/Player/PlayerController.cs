using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [field: SerializeField]
    public PlayerMovementParameters MovementParameters { get; set; }

    [SerializeField] private AudioClip flapClip;


    private bool isDead;
    private Vector3 velocity;
    public Vector3 Velocity => velocity;

    private float zRot;
    public bool IsOnGround { get; private set; }


    private PlayerInputs input;


    private void Awake()
    {
        isDead = false;
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
        if (input.ScreenTap() && !isDead)
        {
            Flap();
            AudioUtility.PlayAudioCue(flapClip);
        }
        return zRot;
    }
    private void ProcessMovement()
    {
        velocity.x = MovementParameters.PlayerSpeed;
        velocity.y -= MovementParameters.Gravity * Time.deltaTime;
    }

    private void ProcessRotation()
    {
        if (velocity.y < 0)
        {
            zRot -= MovementParameters.RotateDownSpeed * Time.deltaTime;
            zRot = Mathf.Max(-90, zRot);
        }
    }
    public void Flap()
    {
        velocity.y = MovementParameters.FlapForce;
        zRot = MovementParameters.FlapAngleDegress;
        
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;            
            input.enabled = false;
            velocity = Vector3.zero;
            AnimationController animation = GetComponent<AnimationController>();
            if (animation != null)
            {
                animation.Die();
            }
            gameMode.GameOver();
        }
    }
    public void IncrementScore()
    {
        gameMode.IncrementScore();
    }
    public void OnHitGround()
    {
        IsOnGround = true;
        enabled = false;
        gameMode.StopAllMoviment();
    }
 
}
