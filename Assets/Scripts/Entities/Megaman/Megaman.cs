using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  enum ANIM_STATE
  {
    SPAWN = 0,
    IDLE,
    MOVE,
    JUMP,
    FALL,
    JUMP_ATTACK,
    ATTACK,
    MOVE_ATTACK,
    DIE
  }

partial class Megaman : Boid
{


  /// <summary>
  /// how fast mega man will move on horizontal direction
  /// </summary>
  [SerializeField]
  private float m_speed;
  public float Speed { get { return m_speed; } }

  /// <summary>
  /// how much time mega man will be on air
  /// </summary>
  [SerializeField]
  private float m_jumpTime;
  public float JumpTime { get { return m_jumpTime; } }

  /// <summary>
  /// how much mega man will jump
  /// </summary>
  [SerializeField]
  private float m_maxJump;
  public float MaxJump { get { return m_maxJump; } }

  /// <summary>
  /// collider
  /// </summary>
  private CapsuleCollider2D m_collider;

  /// <summary>
  /// rigid body
  /// </summary>
  private Rigidbody2D m_rb;

  /// <summary>
  /// int that will control the animation states
  /// </summary>
  private ANIM_STATE m_animState;
  public ANIM_STATE AnimState
  {
    get { return m_animState; }
    set { m_animState = value; }
  }

  /// <summary>
  /// temporal
  /// </summary>
  [SerializeField]
  private bool m_isGround;
  public bool IsGrounded { get { return m_isGround; } }

  [SerializeField]
  private bool m_isWalled;
  public bool IsWalled { get { return m_isWalled; } }

  private LayerMask m_floor;
  private LayerMask m_wall;

  /// <summary>
  /// self animator
  /// </summary>
  private Animator m_animator;

  private float m_directionX;
  public float DirectionX
  {
    get { return m_directionX; }
    internal set
    {
      if (value == 0.0f) return;
      if(value > 0.0f) m_directionX = 1.0f;
      if(value < 0.0f) m_directionX = -1.0f;
      TurnSprite();
    }
  }

  [SerializeField]
  private float m_timeShootBtnPressed;
  public float TimeBtnPressed
  {
    get { return m_timeShootBtnPressed; }
    set { m_timeShootBtnPressed = value; }
  }

  [SerializeField]
  private List<Bullet> m_bullets;

  private int m_indexBullet;

  /// <summary>
  /// Debug stuff
  /// </summary>
  public float airTime = 0;


  private void Awake()
  {
    m_collider = GetComponent<CapsuleCollider2D>();
    m_floor = LayerMask.GetMask("Level");
    m_wall = LayerMask.GetMask("Wall");
    m_animator = GetComponentInChildren<Animator>();
    m_timeShootBtnPressed = 0;
    m_directionX = 1.0f;
    m_indexBullet = -1;

    //Initialize State Machine
    InitStateMachine();
  }

  public void Update()
  {
    
  }

  public void FixedUpdate()
  {
    updateGrounded();

    m_stateMachine.OnState(this);

    Move();
  }

  private void updateGrounded()
  {
    m_isGround = m_collider.IsTouchingLayers(m_floor);
    m_isWalled = m_collider.IsTouchingLayers(m_wall);
  }

  public void setAnim(ANIM_STATE state)
  {
    m_animator.SetInteger("State_enum", (int)state);
  }

  private void TurnSprite()
  {
    var scale = transform.GetChild(0).localScale;
    transform.GetChild(0).localScale = scale;
    scale.x = Mathf.Abs(scale.x) * m_directionX;
    transform.GetChild(0).localScale = scale;
  }

  public void shoot(float time)
  {
    m_indexBullet++;
    if (m_indexBullet > 2) m_indexBullet = 0;

    if (time >= 0.0f && time < 1.0f)
    {
     // Debug.Log("Single shoot");
      m_bullets[m_indexBullet].beeingShot(transform.position, m_directionX);
    }
    else if(time > 1.0f && time < 2.5f)
    {
      //TODO: handle second shoot
     // Debug.Log("Second shoot");
    }
    else if(time > 2.5)
    {
      //TODO: handle massive shoot
     // Debug.Log("Third shoot");
    }
  }

}
