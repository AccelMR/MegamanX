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

  private LayerMask m_floor;

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
      m_directionX = value;
      TurnSprite();
    }
  }

  /// <summary>
  /// Debug stuff
  /// </summary>
  public float airTime = 0;


  private void Awake()
  {
    m_collider = GetComponent<CapsuleCollider2D>();
    m_floor = LayerMask.GetMask("Ground");
    m_animator = GetComponentInChildren<Animator>();

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

}
