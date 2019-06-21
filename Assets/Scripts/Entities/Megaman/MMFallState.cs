using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMFallState : State<Megaman>
{
  /// <summary>
  /// time falling
  /// </summary>
  private float m_timeFalling;

  public MMFallState(StateMachine<Megaman> stateMachine)
      : base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    m_timeFalling = 0.0f;

    entity.setAnim(ANIM_STATE.FALL);
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");

    if (entity.IsGrounded && dirX > 0.0f)
    {
      m_pStateMachine.ToState(entity.moveState, entity);
    }
    else if (entity.IsGrounded)
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

    entity.DirectionX = dirX;

  }

  public override void OnStateUpdate(Megaman entity)
  {
    //Debug
    entity.airTime += Time.fixedDeltaTime;

    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;

    m_timeFalling += Time.fixedDeltaTime;

    float normTime = Mathf.Clamp01(m_timeFalling);
    float lapse = Mathf.Pow(normTime, 2);

    float yPos = Mathf.Lerp(0, entity.MaxJump, normTime);

    if (entity.IsGrounded && dirX > 0.0f)
    {
      m_pStateMachine.ToState(entity.moveState, entity);
    }
    else if (entity.IsGrounded)
    {
      m_pStateMachine.ToState(entity.idleState, entity);
    }
    else if(entity.IsWalled && dirX != 0)
    {
      m_pStateMachine.ToState(entity.wallSlide, entity);
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

    entity.transform.position += new Vector3(0, -yPos, 0);

  }

  public override void OnStateExit(Megaman entity)
  {
    Debug.Log(entity.airTime);

    entity.airTime = 0;
  }
}
