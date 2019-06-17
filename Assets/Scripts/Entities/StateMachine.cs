
using System;


/// <summary>
/// Generic state machine for all of your 'State Machine' needs
/// </summary>
/// <typeparam name="T"></typeparam>
public class StateMachine<T>
{
  private State<T> m_CurrentState;
  private State<T> m_PrevState;

  /// <summary>
  /// Sets initial State of state machine
  /// </summary>
  public void Init(State<T> initialState)
  {
    m_CurrentState = initialState;
    m_PrevState = initialState;
  }

  /// <summary>
  /// Update the given entity according to the current state
  /// </summary>
  /// <param name="entity"></param>
  public void OnState(T entity)
  {
    m_CurrentState.OnStatePreUpdate(entity);
    m_CurrentState.OnStateUpdate(entity);
  }

  /// <summary>
  /// This Function makes a transition between states
  /// </summary>
  /// <param name=""></param>
  public void ToState(State<T> nextState, T entity)
  {
    m_CurrentState.OnStateExit(entity);

    m_PrevState = m_CurrentState;
    m_CurrentState = nextState;

    m_CurrentState.OnStateEnter(entity);
  }

  /// <summary>
  /// checks whether the current state is any of the given compare states
  /// </summary>
  /// <param name="compareStates">states to compare current state with</param>
  /// <returns>boolean telling if current state is any of the compareStates</returns>
  internal bool IsCurrentState(params State<T>[] compareStates)
  {
    foreach (var state in compareStates)
    {
      if (state == m_CurrentState)
      {
        return true;
      }
    }
    return false;
  }
}

