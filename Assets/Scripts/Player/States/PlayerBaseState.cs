public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerController player);
    public abstract void ExitState(PlayerController player);
    public abstract void UpdateState(PlayerController player);
}