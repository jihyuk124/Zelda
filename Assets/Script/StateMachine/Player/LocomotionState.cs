using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("LocomotionState");
        animator.CrossFade(LocomotionHash, crossFadeDuration);
    }

    public override void Update()
    {
        player.HandleMovement();
    }

    public override void FixedUpdate()
    {
    }
}