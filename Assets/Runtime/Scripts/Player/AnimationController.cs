using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void LateUpdate()
    {
        animator.SetFloat("VelocityY", playerController.Velocity.y);
    }

    public void Die()
    {
        animator.enabled = false;
        enabled = false;
    }



}
