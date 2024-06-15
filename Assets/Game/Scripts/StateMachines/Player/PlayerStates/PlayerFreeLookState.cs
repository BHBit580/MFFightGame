using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("VelocityX");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
        
        
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.InputReader.QSpecialAttack += SwitchToQSpecialState;
        stateMachine.InputReader.JumpEvent += SwitchToJumpEvent;
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent += SwitchToHitReactionState;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        MovePlayer(deltaTime);
        HandleFreeLookAnimation(deltaTime);
        ConstrainPlayerPosition();
        SwitchToOtherStates();
    }
    
    private void HandleFreeLookAnimation(float deltaTime)
    {
        float velocityX = stateMachine.InputReader.MovementValue.x;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, velocityX, AnimatorDampTime, deltaTime);
    }

    private void MovePlayer(float deltaTime)
    {
        Vector3 movementVector = new Vector3(0,
            0 , stateMachine.InputReader.MovementValue.x);
        
        stateMachine.transform.Translate(movementVector* (stateMachine.WalkingSpeed * deltaTime));
    }

    #region SwitchingStates
    
    private void SwitchToQSpecialState()
    {
        stateMachine.SwitchState(new PlayerQSpecialAttackState(stateMachine));
    }

    private void SwitchToJumpEvent()
    {
        if (stateMachine.Feet.IsGrounded())
        {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }
    }
    
    private void SwitchToOtherStates()
    {
        if (stateMachine.InputReader.PunchAttack)
        {
            stateMachine.SwitchState(new PlayerPunchComboState(stateMachine , 0));
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockState(stateMachine));
        }

        if (stateMachine.InputReader.IsCrouching)
        {
            stateMachine.SwitchState(new PlayerCrouchState(stateMachine));
        }

        if (stateMachine.InputReader.KickAttack)
        {
            stateMachine.SwitchState(new PlayerKickComboState(stateMachine,0));
        }
    }

    private void SwitchToHitReactionState()
    {
        stateMachine.SwitchState(new PlayerHitReaction(stateMachine));
    }
    
    #endregion
    
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= SwitchToJumpEvent;
        stateMachine.InputReader.QSpecialAttack -= SwitchToQSpecialState;
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent -= SwitchToHitReactionState;
    }

    
}
