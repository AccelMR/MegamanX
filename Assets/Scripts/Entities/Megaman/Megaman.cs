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
    SLIDE,
    DAMAGE,
    DIE
  }

partial class Megaman : Boid
{
  /// <summary>
  /// Health with getter
  /// </summary>
  [SerializeField]
  private int m_health;
  public int Health { get { return m_health; } }

  /// <summary>
  /// if player can be damaged
  /// </summary>
  private bool m_canDamage;

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

  [SerializeField]
  private float m_slideVelocity;
  public float SlideVelocity { get { return m_slideVelocity; } }

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

  private ANIM_STATE m_prevState;

  private float m_recoveryTime;

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
  [SerializeField]
  private Bullet m_greenBullet;
  [SerializeField]
  private Bullet m_blueBullet;


  private int m_indexBullet;

  /// <summary>
  /// Debug stuff
  /// </summary>
  public float airTime = 0;
  public bool addDmg = false;


  private void Awake()
  {
    m_collider = GetComponent<CapsuleCollider2D>();
    m_floor = LayerMask.GetMask("Level");
    m_wall = LayerMask.GetMask("Wall");
    m_animator = GetComponentInChildren<Animator>();
    m_timeShootBtnPressed = 0;
    m_directionX = 1.0f;
    m_indexBullet = -1;
    m_recoveryTime = 0.0f;

    //Instantiate shit, I'm getting sick of this jajajejejejjiji
    m_bullets.Add(GameObject.Find("Bullet").GetComponent<Bullet>());
    m_bullets.Add(GameObject.Find("Bullet (1)").GetComponent<Bullet>());
    m_bullets.Add(GameObject.Find("Bullet (2)").GetComponent<Bullet>());
    m_greenBullet = GameObject.Find("GreenBullet").GetComponent<Bullet>();
    m_blueBullet = GameObject.Find("BlueBullet").GetComponent<Bullet>();

    m_canDamage = true;

    //Initialize State Machine
    InitStateMachine();
  }

  public void Update()
  {
    if(!Input.GetButton("Shoot") )
    {
      if (TimeBtnPressed > 0.9f) shoot(TimeBtnPressed);
      TimeBtnPressed = 0.0f;
    }
    if(addDmg)
    {
      addDamage(1);
    }
  }

  public void FixedUpdate()
  {
    if(m_recoveryTime >= 1.43f)
    {
      m_recoveryTime = 0.0f;
      m_canDamage = true;
      transform.position += new Vector3(-DirectionX * Time.fixedDeltaTime * 2f, 0.0f, 0.0f);
    }

    if (m_canDamage)
    { 
      m_stateMachine.OnState(this);
    }
    else
    {
      m_recoveryTime += Time.fixedDeltaTime;
    }

    Move();
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

  public void shoot(float time, float dir = 0.0f)
  {
    if(dir == 0.0f)
    {
      dir = m_directionX;
    }

    m_indexBullet++;
    if (m_indexBullet > 2) m_indexBullet = 0;

    if (time >= 0.0f && time < 1.0f)
    {
      m_bullets[m_indexBullet].beeingShot(transform.position, dir);
    }
    else if(time > 1.0f && time < 2.5f)
    {
      m_greenBullet.beeingShot(transform.position, dir);
    }
    else if(time > 2.5)
    {
      m_blueBullet.beeingShot(transform.position, dir);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.CompareTag("TriggerLimit")) { return; }

    var v = collision.contacts[0].normal;
    Debug.Log(v);
    if (collision.transform.tag == "Bullet") return;
    if (v == Vector2.right || v == Vector2.left )
    {
      m_isWalled = true;
    }
    if (v == Vector2.up) 
    {
      m_isGround = true;
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.transform.tag == "Bullet") return;
    if (m_isWalled && m_isGround)
    {
      if(Input.GetButton("Jump"))
      {
        m_isGround = false;
      }
      else
      {
        m_isWalled = false;
      }
    }
    else if (m_isWalled) m_isWalled = false;
    else if (m_isGround) m_isGround = false;
  }

  public void addDamage(int dmg)
  {
    if (m_canDamage)
    {
      //setAnim(ANIM_STATE.DAMAGE);
      m_animator.SetTrigger("Dmg");
      m_health -= dmg;
      m_canDamage = false;
    }
  }

}
