using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMFallState : State<Megaman>
{
  public MMFallState(StateMachine<Megaman> stateMachine)
      : base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    entity.setAnim(ANIM_STATE.FALL);
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    if (entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
}

public override void OnStateUpdate(Megaman entity)
{
  if (entity.IsGrounded)
  {
    m_pStateMachine.ToState(entity.idleState, entity);
  }
}
}
