using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Megaman : Boid
{
  [SerializeField]
  private float m_speed;
  public float Speed{get{ return m_speed; }}

  [SerializeField]
  private float m_gravity;
  public float Gravity { get { return m_gravity; }  }


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
