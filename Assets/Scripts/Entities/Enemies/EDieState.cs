using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EDieState : State<Enemy>
{
  /// <summary>
  /// Avoid creating variables on State classes, it could bring problems then
  /// </summary>
  private float m_timeJumping;

  public EDieState(StateMachine<Enemy> stateMachine)
    :base(stateMachine) { }

  public override void OnStateEnter(Enemy entity)
  {

  }

  public override void OnStateExit(Enemy entity)
  {
    //TODO: Just for proves
    entity.VelocityY = 0.0f;
  }

  public override void OnStatePreUpdate(Enemy entity)
  {

  }

  public override void OnStateUpdate(Enemy entity)
  {
    //Play animation, destroy GO
    Debug.Log("Enemy killed");
    GameObject.Destroy(entity.gameObject, 1.0f);



  }
}
