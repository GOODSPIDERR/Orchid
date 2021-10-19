using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerMovementScript player);

    public abstract void UpdateState(PlayerMovementScript player);
}
