using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMWallSlideState : State<Megaman>
{
  public MMWallSlideState(StateMachine<Megaman> stateMachine)
     : base(stateMachine) { }

  public override void OnStatePreUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;
    entity.VelocityX = dirX * entity.Speed;

    if (!entity.IsGrounded && dirX == 0.0f)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if (entity.IsGrounded)
    {
      if (dirX != 0.0f)
      {
        m_pStateMachine.ToState(entity.moveState, entity);
      }
      else
      {
        m_pStateMachine.ToState(entity.idleState, entity);
      }
    }
  }

  public override void OnStateUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;
    entity.VelocityX = dirX * entity.Speed;


    if (entity.IsWalled && dirX != 0.0f)
    {
      entity.transform.position -= new Vector3(0.0f, 0.005f, 0.0f);
    }
    else if(!entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if(entity.IsGrounded)
    {
      if(dirX != 0.0f)
      {
        m_pStateMachine.ToState(entity.moveState, entity);
      }
      else
      {
        m_pStateMachine.ToState(entity.idleState, entity);
      }
    }
  }
}
