using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Enemy : Boid
{
    /// <summary>
    /// Contains information about enemy health and damage
    /// </summary>
    [SerializeField] EnemyData m_data;
    public EnemyData Data { get { return m_data; } }

    /// <summary>
    /// Type of movement the enemy has
    /// </summary>
    [SerializeField] MOVE_TYPE m_moveType;
    public MOVE_TYPE MoveType{ get { return m_moveType; } }


  /// <summary>
  /// How much health the enemy has
  /// </summary>
  [SerializeField] int m_health;
    public int Health { get { return m_health; } }

    /// <summary>
    /// How much damage the enemy deals
    /// </summary>
    [SerializeField] int m_damage;
    public int Damage { get { return m_damage; } }

    /// <summary>
    /// how fast mega man will move on horizontal direction
    /// </summary>
    [SerializeField]
    private float m_speed;
    public float Speed { get { return m_speed; } }

    /// <summary>
    /// gravity applied just when is not grounded
    /// </summary>
    [SerializeField]
    private float m_gravity;
    public float Gravity { get { return m_gravity; } }

    private void Awake()
    {
        m_damage = m_data.Damage;
        m_health = m_data.Health;
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

  public void SeekCall(Vector3 Objective, Vector3 position, float MySpeed)
  {
    StartCoroutine(Seek(Objective, position, MySpeed));
  }
  IEnumerator Seek(Vector3 Objective, Vector3 position, float MySpeed)
  {
    yield return new WaitForSeconds(0.75f);

    Vector3 SeekVector = Objective - position;
    SeekVector.Normalize();
    SeekVector *= MySpeed * SeekVector.normalized.magnitude;
    VelocityX = SeekVector.x;
  }




}
