using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EIdleState : State<Enemy>
{
  public EIdleState(StateMachine<Enemy> stateMachine)
  : base(stateMachine) { }

  public override void OnStateEnter(Enemy character)
  {
  }

  public override void OnStatePreUpdate(Enemy entity)
  {
  }

  public override void OnStateUpdate(Enemy entity)
  {
    m_pStateMachine.ToState(entity.moveState, entity);
  }
}
