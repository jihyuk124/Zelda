using UnityEngine;

public class GatheringState : BaseState
{
    public GatheringState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("GatheringState");
        animator.CrossFade(Gathering, crossFadeDuration); // A(�̵� �Ǵ� IDLE) -> B(ä��)
    }

    public override void OnExit()
    {
        player.OnFinishCollect();
    }
}