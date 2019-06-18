using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMIdleState : State<Megaman>
{
  public MMIdleState(StateMachine<Megaman> stateMachine)
  : base(stateMachine) { }

  public override void OnStateEnter(Megaman character)
  {
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
  }

  public override void OnStateUpdate(Megaman entity)
  {
    // Check inputs every time
    if (Input.GetButtonDown("Jump"))
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }

    if(Input.GetAxisRaw("Horizontal") != 0)
    {
      m_pStateMachine.ToState(entity.moveState, entity);
    }
  }
}
