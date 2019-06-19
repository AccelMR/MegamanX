using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Enemy : Boid
{
  //Object of the StateMachine Class made with sfCharacter Template
  private StateMachine<Enemy> m_stateMachine;

  /// <summary>
  /// States declarations will be here
  /// </summary>
  public EIdleState idleState;
  public EDieState  dieState;
  public EMoveState moveState;
  /// <summary>
  /// Initialize State Machine and all states
  /// </summary>
  public virtual void InitStateMachine()
  {
    m_stateMachine = new StateMachine<Enemy>();

    idleState = new EIdleState(m_stateMachine);
    moveState = new EMoveState(m_stateMachine);
    dieState = new EDieState(m_stateMachine);

        /// First state, or initial state
        m_stateMachine.Init(idleState);
  }
}
