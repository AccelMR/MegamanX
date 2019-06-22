
/// <summary>
/// Generic state interface to use in conjunction with the StateMachine class
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class State<T>
{
  public State(StateMachine<T> stateMachine)
  {
    m_pStateMachine = stateMachine;
  }

  /// <summary>
  /// Invoked each time the state is entered
  /// </summary>
  /// <param name="entity"></param>
  public virtual void OnStateEnter(T entity) { }

  /// <summary>
  /// Invoked right before OnStateUpdate.
  /// Meant to change the Current state in case it must be updated.
  /// </summary>
  /// <param name="entity"></param>
  public abstract void OnStatePreUpdate(T entity);

  /// <summary>
  /// Invoked each time the state is updated
  /// </summary>
  /// <param name="entity"></param>
  public abstract void OnStateUpdate(T entity);

  /// <summary>
  /// Invoked each time the state is exited
  /// </summary>
  /// <param name="entity"></param>
  public virtual void OnStateExit(T entity) { }

  protected readonly StateMachine<T> m_pStateMachine;
}
