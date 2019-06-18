using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Megaman : Boid
{
  //Object of the StateMachine Class made with sfCharacter Template
  private StateMachine<Megaman> m_stateMachine;

  /// <summary>
  /// States declarations will be here
  /// </summary>
  public MMIdleState idleState;

  /// <summary>
  /// Initialize State Machine and all states
  /// </summary>
  private void InitStateMachine()
  {
    m_stateMachine = new StateMachine<Megaman>();

    idleState = new MMIdleState(m_stateMachine);

    /// First state, or initial state
    m_stateMachine.Init(idleState);
  }
}
