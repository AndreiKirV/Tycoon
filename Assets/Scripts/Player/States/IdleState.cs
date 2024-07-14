public class IdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.PlayAnimation("Idle");
    }

    public override void ExitState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        if (player.MoveVector.magnitude != 0 && player.CurrentSpeed != player.RunSpeed)
        {
            player.SwitchState(player.WalkState);
        }
        else if (player.MoveVector.magnitude != 0)
        {
            player.SwitchState(player.RunState);
        }
    }
}