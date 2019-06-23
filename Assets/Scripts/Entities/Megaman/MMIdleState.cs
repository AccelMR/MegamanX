using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMIdleState : State<Megaman>
{
  public MMIdleState(StateMachine<Megaman> stateMachine)
  : base(stateMachine) { }

  public override void OnStateEnter(Megaman character)
  {
    character.setAnim(ANIM_STATE.IDLE);
    character.VelocityX = 0.0f;
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    entity.VelocityX = 0.0f;

    // Check inputs every time
    if (Input.GetButtonDown("Jump") && entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }
    else if (Input.GetAxisRaw("Horizontal") != 0 )
    {
      m_pStateMachine.ToState(entity.moveState, entity);
    }
    else if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }
    else if (!Input.GetButtonDown("Jump") && !entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }
  }

  public override void OnStateUpdate(Megaman entity)
  {
    entity.VelocityX = 0.0f;

    // Check inputs every time
    if (Input.GetButtonDown("Jump") && entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.jumpState, entity);
    }
    else if (Input.GetAxisRaw("Horizontal") != 0)
    {
      m_pStateMachine.ToState(entity.moveState, entity);
    }
    else if(Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if(Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }

  }
}
