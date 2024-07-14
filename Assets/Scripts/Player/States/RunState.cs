public class RunState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.PlayAnimation("Run");
    }

    public override void ExitState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        if (player.CurrentSpeed != player.RunSpeed && player.MoveVector.magnitude != 0)
        {
            player.SwitchState(player.WalkState);
        }
        else if (player.MoveVector.magnitude == 0)
        {
            player.SwitchState(player.IdleState);
        }
        else
        {
            player.Move();
        }
    }
}
