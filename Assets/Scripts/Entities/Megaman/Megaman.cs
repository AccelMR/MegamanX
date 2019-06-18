using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Megaman : Boid
{
  /// <summary>
  /// how fast mega man will move on horizontal direction
  /// </summary>
  [SerializeField]
  private float m_speed;
  public float Speed{get{ return m_speed; }}

  /// <summary>
  /// gravity applied just when is not grounded
  /// </summary>
  [SerializeField]
  private float m_gravity;
  public float Gravity { get { return m_gravity; }  }

  /// <summary>
  /// how much time mega man will be on air
  /// </summary>
  [SerializeField]
  private float m_jumpTime;
  public float JumpTime { get { return m_jumpTime; } }

  /// <summary>
  /// how much mega man will jump
  /// </summary>
  [SerializeField]
  private float m_jumpForce;
  public float JumpForce { get { return m_jumpForce; } }

  private void Awake()
  {
    //Initialize State Machine
    InitStateMachine();
  }

  public void Update()
  {
    
  }

  public void FixedUpdate()
  {
    m_stateMachine.OnState(this);

    Move();
  }

}
