using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_TYPE
{
  Basic,
  Seek_X,
  Bee
}

class EMoveState : State<Enemy>
{
  public EMoveState(StateMachine<Enemy> stateMachine)
    : base(stateMachine) { }

  bool isReady;
  bool isLeaving;
  float reloadTime;
  public override void OnStateEnter(Enemy entity)
  {
    isReady = true;
    isLeaving = false;
    reloadTime = 1.5f;
  }
  public override void OnStatePreUpdate(Enemy entity)
  {
    
  }

  public override void OnStateUpdate(Enemy entity)
  {
    switch (entity.MoveType)
    {
      case MOVE_TYPE.Basic:

        BasicMove(entity);
        break;
      case MOVE_TYPE.Seek_X:
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        entity.SeekCall(target, entity.transform.position, entity.Speed);
          /*reloadTime -= Time.deltaTime;
          if(reloadTime <= 0)
          {
            isReady = true;
            reloadTime = 1.5f;
          }
          if (isReady)
          {
            Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
            SeekX(target, entity.transform.position, entity.Speed, entity);
            isReady = false;
          }*/
        break;
      case MOVE_TYPE.Bee:
        //Divide into parts
        ///Go to target pos.
        Vector3 targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (isReady && !isLeaving)
        {
          BeeMove(targetPos, entity);
        }
        ///Shoot mines.
        if (!isReady && !isLeaving)
        {
          //Shoot some shit.
          isLeaving = true;
        }
        ///Leave.
        if (!isReady && isLeaving)
        {
          entity.SeekCall(targetPos-new Vector3(5.5f,0,0), entity.transform.position, entity.Speed);
          isReady = true;
        }
        ///Go up.
        if (isReady && isLeaving)
        {
          entity.SeekCall(targetPos - new Vector3(5.5f, 2, 0), entity.transform.position, entity.Speed);

        }

          break;
      default:
        BasicMove(entity);
        break;
    }
    if (entity.Health <= 0)
    {
      m_pStateMachine.ToState(entity.dieState, entity);
    }
  }

  void BasicMove(Enemy entity)
  {
    float dir = -1;
    entity.VelocityX = dir * entity.Speed;

    
  }
  
  void SeekX(Vector3 Objective, Vector3 position, float MySpeed, Enemy entity)
  {
    Vector3 SeekVector = Objective - position;
    SeekVector.Normalize();
    SeekVector *= MySpeed * SeekVector.normalized.magnitude;
    Debug.Log("Velocity X = " + SeekVector.x);
    entity.VelocityX =  SeekVector.x;
  }
  
  void BeeMove(Vector3 target, Enemy entity)
  {
    float distance = (target - entity.transform.position).x;
    if(entity.transform.position.x >= target.x - 0.1f && entity.transform.position.x <= target.x + 0.1f)
    {
      SeekX(target, entity.transform.position, entity.Speed, entity);
    }
    else
    {
      isReady = false;
    }
  }
}

