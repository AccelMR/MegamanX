using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMMoveState : State<Megaman>
{
  public MMMoveState(StateMachine<Megaman> stateMachine)
    :base(stateMachine) { }

  public override void OnStatePreUpdate(Megaman entity)
  {
  }

  public override void OnStateUpdate(Megaman entity)
  {
    entity.VelocityX += .2f;
  }
}
