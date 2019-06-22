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
      entity.setAnim(ANIM_STATE.ATTACK);
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.setAnim(ANIM_STATE.ATTACK);
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }
    if (!entity.IsGrounded && !Input.GetButtonDown("Jump"))
    {
      m_pStateMachine.ToState(entity.fallState, entity);
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
      entity.setAnim(ANIM_STATE.ATTACK);
      entity.shoot(0.0f);
    }
    else if(Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.setAnim(ANIM_STATE.ATTACK);
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }

  }
}
