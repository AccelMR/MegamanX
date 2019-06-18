using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMJumpState : State<Megaman>
{
  /// <summary>
  /// Avoid creating variables on State classes, it could bring problems then
  /// </summary>
  private float m_timeJumping;

  public MMJumpState(StateMachine<Megaman> stateMachine)
    :base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    entity.setAnim(ANIM_STATE.JUMP);
    m_timeJumping = entity.JumpTime;
  }

  public override void OnStateExit(Megaman entity)
  {
    //TODO: Just for proves
    entity.VelocityY = 0.0f;
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");

    entity.VelocityY = entity.JumpForce;
    entity.VelocityX = dirX * entity.Speed;
    entity.DirectionX = dirX;

  }

  public override void OnStateUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;

    if (Input.GetButton("Jump") && m_timeJumping > 0.0f)
    {
      entity.VelocityY = entity.JumpForce;
      entity.VelocityX = dirX * entity.Speed;
      
      /// reduce time
      m_timeJumping--;
    }
    else
    {//TODO: shouldn't go to this state, just for proves
      m_pStateMachine.ToState(entity.moveState , entity);
    }




  }
}
