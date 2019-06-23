using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MMJumpState : State<Megaman>
{
  /// <summary>
  /// Avoid creating variables on State classes, it could bring problems then
  /// </summary>
  private float m_timeJumping;

  private Vector3 m_startPos;

  public MMJumpState(StateMachine<Megaman> stateMachine)
    :base(stateMachine) { }

  public override void OnStateEnter(Megaman entity)
  {
    entity.setAnim(ANIM_STATE.JUMP);
    m_timeJumping = 0.4f;
    m_startPos = entity.Position;
    entity.SourceAudi.PlayOneShot(entity.m_audioJump, 8.0f);
  }

  public override void OnStateExit(Megaman entity)
  {
    //TODO: Just for proves
    entity.VelocityY = 0.0f;
  }

  public override void OnStatePreUpdate(Megaman entity)
  {
    var dirX = Input.GetAxisRaw("Horizontal");

    entity.VelocityX = dirX * entity.Speed;
    entity.DirectionX = dirX;

    if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }
  }

  public override void OnStateUpdate(Megaman entity)
  {
    //Debug
    entity.airTime += Time.fixedDeltaTime;

    var dirX = Input.GetAxisRaw("Horizontal");
    entity.DirectionX = dirX;
    m_timeJumping -= Time.fixedDeltaTime;

    if (Input.GetButtonDown("Shoot"))
    {
      entity.shoot(0.0f);
    }
    else if (Input.GetButton("Shoot"))
    {
      entity.TimeBtnPressed += Time.fixedDeltaTime;
    }

    if (Input.GetButtonUp("Shoot") && entity.TimeBtnPressed > 0.98f)
    {
      entity.shoot(entity.TimeBtnPressed);
      entity.TimeBtnPressed = 0.0f;
    }

    if (Input.GetButton("Jump") && m_timeJumping > 0.0f)
    {

      float normTime = Mathf.Clamp01(m_timeJumping);
      float lapse = Mathf.Pow(normTime, 2);

      float yPos = Mathf.Lerp(0, entity.MaxJump, normTime);
      entity.transform.position += new Vector3(0, yPos, 0);
      //entity.VelocityY = yPos;

    }
    else
    {
      m_pStateMachine.ToState(entity.fallState , entity);
    }



  }
}
