public class WalkState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.PlayAnimation("Walk");
    }

    public override void ExitState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        if (player.MoveVector.magnitude == 0)
        {
            player.SwitchState(player.IdleState);
        }
        else
        {
            player.Move();
        }
    }
}