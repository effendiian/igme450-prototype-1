using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// StateManager manages stack of GameStates.
/// </summary>
public class StateManager : MonoBehaviour
{

    #region Fields

    /// <summary>
    /// Stack of GameState objects.
    /// </summary>
    [SerializeField, ReadOnly, Label("States"), Tooltip("Collection of game states managed by this state manager.")]
    private Stack<GameState> _states;

    #endregion

    #region Properties

    /// <summary>
    /// Readonly reference to the stack of game states.
    /// </summary>
    public Stack<GameState> States => this._states;

    /// <summary>
    /// If states exist in the stack, return true.
    /// </summary>
    public bool HasStates => (this.States != null) && (this.States.Count > 0);

    /// <summary>
    /// Get reference to the state at the top of the stack. If empty, returns null.
    /// </summary>
    public GameState CurrentState => (this.HasStates) ? this.States.Peek() : null;

    #endregion

    #region MonoBehaviours
    
    /// <summary>
    /// OnAwake, initialize the state collection.
    /// </summary>
    public virtual void Start() => this._states = this._states ?? new Stack<GameState>();

    #endregion

    #region Serivce Methods

    /// <summary>
    /// Check if input state matches current state.
    /// </summary>
    /// <param name="state">State to check.</param>
    /// <returns>True if match is found.</returns>
    public bool IsState(GameState state) => (state != null && state == this.CurrentState);

    /// <summary>
    /// Clear the state manager of all its states.
    /// </summary>
    public void Cleanup()
    {
        // While it has states, cleanup the states and clear the stack.
        while (this.HasStates)
        {
            this.CurrentState.Cleanup();
            this.States.Pop();
        }
    }

    /// <summary>
    /// Cleanup the current state and then store and initiate the new state.
    /// </summary>
    /// <param name="state">State to change to.</param>
    public void ChangeState(GameState state)
    {
        if (this.HasStates)
        {
            this.CurrentState.Cleanup();
            this.States.Pop();
        }

        this.States.Push(state);
        this.CurrentState.Init(this);
    }

    /// <summary>
    /// Pause the current state (if one exists), then push and initialize the new state on the stack.
    /// </summary>
    /// <param name="state">State to initialize.</param>
    public void PushState(GameState state)
    {
        // Pause the current state.
        if (this.HasStates)
        {
            this.CurrentState.Pause();
        }

        // Store and init the new state.
        this.States.Push(state);
        this.CurrentState.Init(this);
    }

    /// <summary>
    /// Cleanup the current state and resume the previous state (if any exist).
    /// </summary>
    public void PopState()
    {
        if (this.HasStates)
        {
            this.CurrentState.Cleanup();
            this.States.Pop();
        }

        if (this.CurrentState)
        {
            this.CurrentState.Resume();
        }
    }

    #endregion

}
