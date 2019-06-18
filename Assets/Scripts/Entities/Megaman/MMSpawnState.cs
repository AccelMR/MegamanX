using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMSpawnState : State<Megaman>
{
  public MMSpawnState(StateMachine<Megaman> stateMachine)
    : base(stateMachine) { }

  public override void OnStatePreUpdate(Megaman entity)
  {

  }

  public override void OnStateUpdate(Megaman entity)
  {
    if(entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
  }

  public override void OnStateExit(Megaman entity)
  {
  }
}
