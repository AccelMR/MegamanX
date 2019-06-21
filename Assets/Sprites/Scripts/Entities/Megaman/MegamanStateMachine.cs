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
  public MMJumpState jumpState;
  public MMMoveState moveState;
  public MMSpawnState spawnState;
  public MMFallState fallState;

  /// <summary>
  /// Initialize State Machine and all states
  /// </summary>
  private void InitStateMachine()
  {
    m_stateMachine = new StateMachine<Megaman>();

    spawnState = new MMSpawnState(m_stateMachine);
    idleState  = new MMIdleState(m_stateMachine);
    jumpState  = new MMJumpState(m_stateMachine);
    moveState  = new MMMoveState(m_stateMachine);
    fallState  = new MMFallState(m_stateMachine);

    /// First state, or initial state
    m_stateMachine.Init(spawnState);
  }
}
