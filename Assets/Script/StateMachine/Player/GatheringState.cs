using UnityEngine;

public class GatheringState : BaseState
{
    public GatheringState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("GatheringState");
        animator.CrossFade(Gathering, crossFadeDuration); // A(이동 또는 IDLE) -> B(채집)
    }

    public override void OnExit()
    {
        player.OnFinishCollect();
    }
}