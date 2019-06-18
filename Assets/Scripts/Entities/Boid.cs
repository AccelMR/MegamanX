using UnityEngine;

/// <summary>
/// Generic Boid class for all the entities
/// </summary>
public abstract class Boid : MonoBehaviour
{
  /*public float m_fMaxSpeed;
  public float m_fMaxForce;*/

  [SerializeField]
  protected bool interpolateMovement;

  public bool InterpolateMovement
  {
    get { return interpolateMovement; }
  }

  private Vector2 m_v2Velocity;
  private Vector2 m_v2TotalForce;
  private float m_poiseAmount;

  private Vector2 m_v2LastPosition;


  /// <summary>
  /// Getter and Setter of the Poise of the boid
  /// </summary>
  public float PoiseAmount
  {
    get { return m_poiseAmount; }
    set { m_poiseAmount = value; }
  }

  /// <summary>
  /// Getter and Setter of the Position of the boid
  /// </summary>
  public Vector2 Position
  {
    get { return transform.position; }
    set { transform.position = value; }
  }

  /// <summary>
  /// Getter and Setter of the direction of the boid
  /// </summary>
  public Vector2 Direction
  {
    get { return m_v2Velocity.normalized; }
    set { m_v2Velocity = m_v2Velocity.magnitude * value; }
  }

  /// <summary>
  /// Getter and Setter of the velocity of the boid
  /// </summary>
  public Vector2 Velocity
  {
    get { return m_v2Velocity; }
    set { m_v2Velocity = value; }
  }

  /// <summary>
  /// Getter and Setter of the velocity of the boid in X
  /// </summary>
  public float VelocityX
  {
    get { return m_v2Velocity.x; }
    set { m_v2Velocity.x = value; }
  }

  /// <summary>
  /// Getter and Setter of the velocity of the boid in Y
  /// </summary>
  public float VelocityY
  {
    get { return m_v2Velocity.y; }
    set { m_v2Velocity.y = value; }
  }

  /// <summary>
  /// Getter and Setter of the speed of the boid
  /// </summary>
  public float Speed
  {
    get { return m_v2Velocity.magnitude; }
    set { m_v2Velocity = m_v2Velocity.normalized * value; }
  }

  /// <summary>
  /// Getter of the last position of the boid
  /// </summary>
  public Vector2 LastPosition
  {
    get { return m_v2LastPosition; }
  }

  /// <summary>
  /// This function adds force to the total force of the boid
  /// </summary>
  /// <param name="force"></param>
  public void AddForce(Vector2 force)
  {
    m_v2TotalForce += force;
  }

  /// <summary>
  /// This function is called from the child class FixedUpdate to move the Boid
  /// </summary>
  protected void Move()
  {
    m_v2LastPosition = transform.position;
    m_v2Velocity += m_v2TotalForce * Time.fixedDeltaTime;
    transform.position += (Vector3)m_v2Velocity * Time.fixedDeltaTime;
  }
}