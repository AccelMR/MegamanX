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
  private bool m_canMove;

  /// <summary>
  /// If Megaman gonna flash after taking damage
  /// </summary>
  private bool m_canRecieveDmg;

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

  /// <summary>
  /// How much time can't move after getting a hit
  /// </summary>
  private float m_attkAnimTime;

  /// <summary>
  /// How much time is going to be invulnerable
  /// </summary>
  private float m_invulnerabilityTime;

  /// <summary>
  /// Current sprite
  /// </summary>
  private SpriteRenderer m_sprite;
  public SpriteRenderer Esprait
  {
    get
    {
      if(m_sprite == null)
      {
        m_sprite = GetComponentInChildren<SpriteRenderer>();
      }
      return m_sprite;

    }
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

  public bool m_inclined = false;

  private LayerMask m_floor;
  private LayerMask m_wall;

  /// <summary>
  /// self animator
  /// </summary>
  private Animator m_animator;

  /// <summary>
  ///  Audio source
  /// </summary>
  private AudioSource m_audioSource;
  public AudioSource SourceAudi { get { return m_audioSource; } }



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

  /// <summary>
  /// Here is where audio goes
  /// We should avoid get stuff like that but now it's too late :C
  /// </summary>
  public AudioClip m_fShoot;
  public AudioClip m_sShoot;
  public AudioClip m_audioJump;
  public AudioClip m_audioDamage;
  public AudioClip m_audioCharge;
  public AudioClip m_audioDeath;


  private void Awake()
  {
    m_collider = GetComponent<CapsuleCollider2D>();
    m_floor = LayerMask.GetMask("Level");
    m_wall = LayerMask.GetMask("Wall");
    m_animator = GetComponentInChildren<Animator>();
    m_timeShootBtnPressed = 0;
    m_directionX = 1.0f;
    m_indexBullet = -1;
    m_attkAnimTime = 0.0f;
    m_invulnerabilityTime = 100.0f;
    
    //Sound
    m_audioSource = GetComponent<AudioSource>();

    //Instantiate shit, I'm getting sick of this
    m_bullets.Add(GameObject.Find("Bullet").GetComponent<Bullet>());
    m_bullets.Add(GameObject.Find("Bullet (1)").GetComponent<Bullet>());
    m_bullets.Add(GameObject.Find("Bullet (2)").GetComponent<Bullet>());
    m_greenBullet = GameObject.Find("GreenBullet").GetComponent<Bullet>();
    m_blueBullet = GameObject.Find("BlueBullet").GetComponent<Bullet>();

    //Stuff for damage
    m_canMove = true;
    m_canRecieveDmg = true;

    //Initialize State Machine
    InitStateMachine();
  }

  public void Update()
  {
    //Just a recheck if shoot ain't pressed, if not it'll call shoot
    if(!Input.GetButton("Shoot") )
    {
      if (TimeBtnPressed > 0.9f) shoot(TimeBtnPressed);
      TimeBtnPressed = 0.0f;
    }

    //Debug editor button to add damage
    if(addDmg)
    {
      addDamage(16);
    }

    //Deal with invulnerability frames. I didn't do it well but that was the "fastest" thoughts
    //if you know a better way go ahead and fix it up :C
    if(m_invulnerabilityTime >= 0.0f && m_invulnerabilityTime < 10.0f)
    {
      Esprait.enabled = !Esprait.enabled;
      m_invulnerabilityTime -= Time.deltaTime;
    }
    else if(m_invulnerabilityTime < 0.0f)
    {
      m_canRecieveDmg = true;
      Esprait.enabled = true;
      m_invulnerabilityTime = 100.0f;
    }

  }

  public void FixedUpdate()
  {
    //Here basically takes time until Megaman can move
    if (m_attkAnimTime >= 1.43f)
    {
      m_attkAnimTime = 0.0f;
      m_canMove = true;
      m_invulnerabilityTime = 1.0f;
    }

    //If he can move then will call its state. 
    //NOTE: almost sure that it shouldn't be done like that
    if (m_canMove)
    { 
      m_stateMachine.OnState(this);
    }
    else
    {
      m_attkAnimTime += Time.fixedDeltaTime;
      transform.position += new Vector3(-DirectionX * Time.fixedDeltaTime *.2f, 0.0f, 0.0f);
    }

    //Boid move function
    Move();
  }

  /// <summary>
  /// Change Megaman animation
  /// </summary>
  /// <param name="state">enum of state where it'll change</param>
  public void setAnim(ANIM_STATE state)
  {
    m_animator.SetInteger("State_enum", (int)state);
  }

  /// <summary>
  /// Flips sprites
  /// </summary>
  private void TurnSprite()
  {
    var scale = transform.GetChild(0).localScale;
    transform.GetChild(0).localScale = scale;
    scale.x = Mathf.Abs(scale.x) * m_directionX;
    transform.GetChild(0).localScale = scale;
  }

  /// <summary>
  /// Shoot function
  /// </summary>
  /// <param name="time">how much time shoot button have been pressed</param>
  /// <param name="dir">direction of where bullet will go, if you don't send
  /// anything then direction will be same as Megaman direction</param>
  /// 
  /// Bug:when it's coming from Wall slide state sometimes it shoots to the opposite direction. 
  /// It is because it takes the shoot from Update and not from state
  public void shoot(float time, float dir = 0.0f)
  {
    if(dir == 0.0f)
    {
      dir = m_directionX;
    }

    triggerAttckAnim();
    m_indexBullet++;
    if (m_indexBullet > 2) m_indexBullet = 0;

    if (time >= 0.0f && time < 1.0f)
    {
      m_audioSource.PlayOneShot(m_sShoot, 0.5f);
      m_bullets[m_indexBullet].beeingShot(transform.position, dir);
    }
    else if(time > 1.0f && time < 2.5f)
    {
      m_audioSource.PlayOneShot(m_fShoot, 0.3f);
      m_greenBullet.beeingShot(transform.position, dir);
    }
    else if(time > 2.5)
    {
      m_audioSource.PlayOneShot(m_fShoot, 0.5f);
      m_blueBullet.beeingShot(transform.position, dir);
    }
  }

  /// <summary>
  /// On collision enter
  /// </summary>
  /// <param name="collision">collider</param>
  /// NOTE: depending on the projection vector is which flag is active
  private void OnCollisionEnter2D(Collision2D collision)
  {
    //Don't use this tag on anything, just for camera stuff
    if(collision.collider.CompareTag("TriggerLimit")) { return; }


    var v = collision.contacts[0].normal;
    float a = Vector2.Angle(v, Vector2.right);

    Debug.Log(a);
    if (collision.transform.tag == "Bullet") return;
    if(a > 45 && a < 135)
    {
      Debug.Log("Grounded set");
      m_inclined = ((a > 45 && a < 90) || (a > 90 && a < 135)) ? true : false;
      m_isGround = true;
    }
    if (v == Vector2.right || v == Vector2.left )
    {
      Debug.Log("Walled set");
      m_isWalled = true;
    }
  }

  /// <summary>
  /// Collision Exit
  /// </summary>
  /// <param name="collision"></param>
  /// NOTE: this function is bullshit, gotta fix it up TODO:
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


  /// <summary>
  /// add damage to Megaman
  /// </summary>
  /// <param name="dmg">how much health it'll take of Megaman</param>
  public void addDamage(int dmg)
  {
    if (m_canRecieveDmg)
    {
      m_health -= dmg;
      VelocityX = 0;
      m_canMove = false;
      m_canRecieveDmg = false;
      if(m_health <= 0)
      {
        dead();
        return;
      }
      m_animator.SetTrigger("Dmg");
      m_audioSource.PlayOneShot(m_audioDamage, 2);
    }
  }

  private void dead()
  {//TODO: deal dead
    Debug.Log("M0ristes buey");
    m_audioSource.PlayOneShot(m_audioDeath, 0.5f);
  }

  private void triggerAttckAnim()
  {
    if(m_stateMachine.IsCurrentState(idleState))
    {
      m_animator.SetTrigger("attack");
    }
    else if(m_stateMachine.IsCurrentState(moveState))
    {
      m_animator.SetTrigger("movAttck");
    }
    else if(m_stateMachine.IsCurrentState(jumpState) || m_stateMachine.IsCurrentState(fallState))
    {
      m_animator.SetTrigger("airAttck");
    }
    else if(m_stateMachine.IsCurrentState(wallSlide))
    {
      m_animator.SetTrigger("slideAttck");
    }
  }

}
