using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMMoveState : State<Megaman>
{
  public MMMoveState(StateMachine<Megaman> stateMachine)
    : base(stateMachine) { }

  public override void OnStatePreUpdate(Megaman entity)
  {
    float dir = Input.GetAxisRaw("Horizontal");
    entity.VelocityX = dir * entity.Speed;
  }

  public override void OnStateUpdate(Megaman entity)
  {
    float dir = Input.GetAxisRaw("Horizontal");
    entity.VelocityX = dir * entity.Speed;

    if (dir == 0.0f)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if (Input.GetButtonDown("Jump"))
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }

  }
}
