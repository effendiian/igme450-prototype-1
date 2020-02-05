using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// GameState implementations handles event flow for a specific context.
/// </summary>
public class GameState : MonoBehaviour
{

    #region Data Member

    /// <summary>
    /// State manager.
    /// </summary>
    [SerializeField, Required, Label("State Manager"), Tooltip("State Manager this state will be handled by.")]
    protected StateManager manager;

    /// <summary>
    /// Paused flag.
    /// </summary>
    [SerializeField, Label("Paused?"), Tooltip("Flag checking if state is paused.")]
    private bool m_paused = false;

    /// <summary>
    /// Debug mode flag.
    /// </summary>
    [SerializeField, Label("Debug Mode?"), Tooltip("Print debug messages.")]
    private bool m_debug = false;
    
    /// <summary>
    /// Check if the state is paused.
    /// </summary>
    public bool IsPaused => this.m_paused;

    /// <summary>
    /// Debug mode flag.
    /// </summary>
    public bool InDebug => this.m_debug;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Update the game state.
    /// </summary>
    public virtual void Update()
    {
        if (!this.IsPaused && this.manager.IsState(this))
        {
            this.HandleEvents();
            Debug.Log($"[{manager.name}.{gameObject.name}]: Updating the game state.");
        }
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Initialize the game state.
    /// </summary>
    public virtual void Init(StateManager stateManager)
    {
        Debug.Log($"[{manager.name}.{gameObject.name}]: Initializing the game state.");
        this.manager = stateManager;
        this.Resume();
    }

    /// <summary>
    /// Cleaning up the game state.
    /// </summary>
    public virtual void Cleanup()
    {
        Debug.Log($"[{manager.name}.{gameObject.name}]: Ending and cleaning up the game state.");
        this.Pause();
    }

    /// <summary>
    /// Pause GameState behaviour so that it can be resumed.
    /// </summary>
    public virtual void Pause() => this.m_paused = true;

    /// <summary>
    /// Resume GameState behaviour so that it can continue from where it was paused.
    /// </summary>
    public virtual void Resume() => this.m_paused = false;

    /// <summary>
    /// Handle the user and game events.
    /// </summary>
    public virtual void HandleEvents()
    {
        if (!this.IsPaused && this.InDebug && this.manager.IsState(this))
        {
            Debug.Log($"[{manager.name}.{gameObject.name}]: Handling user and game events.");
        }
    }

    /// <summary>
    /// Change this state to the input state, via the manager.
    /// </summary>
    /// <param name="state">State to change to.</param>
    public void ChangeState(GameState state) => this.manager.ChangeState(state);

    #endregion

}
