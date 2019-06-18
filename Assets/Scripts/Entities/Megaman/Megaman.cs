﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Megaman : Boid
{
  public float m_direction;

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
