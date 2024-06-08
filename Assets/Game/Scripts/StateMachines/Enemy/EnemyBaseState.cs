using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public float DistanceBetweenPlayerAndEnemy()
    {
        return Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position);
    }
}