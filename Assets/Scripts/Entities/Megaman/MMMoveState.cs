using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMMoveState : State<Megaman>
{
  public MMMoveState(StateMachine<Megaman> stateMachine)
    : base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    entity.setAnim(ANIM_STATE.MOVE);
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    float dir = Input.GetAxisRaw("Horizontal");

    entity.VelocityX = dir * entity.Speed;
    entity.DirectionX = dir;

    if (dir == 0.0f)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if (Input.GetButtonDown("Jump") && entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }
    else if (!entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if (dir == 0.0f)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }
    else if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 1.0f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }
  }

  public override void OnStateUpdate(Megaman entity)
  {
    float dir = Input.GetAxisRaw("Horizontal");

    entity.VelocityX = dir * entity.Speed;

    if (dir == 0.0f)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if (Input.GetButtonDown("Jump") && entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }
    else if (!entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if (dir == 0.0f)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }
    else if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 1.0f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }

  }
}
