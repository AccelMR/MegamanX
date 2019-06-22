using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMWallSlideState : State<Megaman>
{
  public MMWallSlideState(StateMachine<Megaman> stateMachine)
     : base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    entity.setAnim(ANIM_STATE.SLIDE);
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;
    entity.VelocityX = dirX * entity.Speed;

    if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f, -dirX);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }
    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed, -dirX);
      entity.TimeBtnPressed = 0.0f;
    }


    if (!entity.IsGrounded && dirX == 0.0f)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if (entity.IsGrounded)
    {
      if (dirX != 0.0f)
      {
        m_pStateMachine.ToState(entity.moveState, entity);
      }
      else
      {
        m_pStateMachine.ToState(entity.idleState, entity);
      }
    }


  }

  public override void OnStateUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;
    entity.VelocityX = dirX * entity.Speed;

    if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f, -dirX);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }
    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed, -dirX);
      entity.TimeBtnPressed = 0.0f;
    }

    if (entity.IsWalled && dirX != 0.0f)
    {
      entity.transform.position -= new Vector3(0.0f, entity.SlideVelocity, 0.0f);
    }
    else if(!entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.fallState, entity);
    }
    else if(entity.IsGrounded)
    {
      if(dirX != 0.0f)
      {
        m_pStateMachine.ToState(entity.moveState, entity);
      }
      else
      {
        m_pStateMachine.ToState(entity.idleState, entity);
      }
    }

    if(Input.GetButtonDown("Jump"))
    {
      entity.VelocityX = -dirX * 8.0f;
      m_pStateMachine.ToState(entity.jumpState, entity);
    }

  }
}
