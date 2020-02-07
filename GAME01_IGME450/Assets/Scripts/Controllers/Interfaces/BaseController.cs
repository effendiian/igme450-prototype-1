using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller state.
/// </summary>
public enum ControllerState
{

    /// <summary>
    /// The state is stopped.
    /// </summary>
    Stopped,

    /// <summary>
    /// The state is running.
    /// </summary>
    Playing,

    /// <summary>
    /// The state is paused.
    /// </summary>
    Paused,
}

/// <summary>
/// BaseController provides reusable functionality between controllers.
/// </summary>
public abstract class BaseController : MonoBehaviour
{

    #region Data Members
    
    /// <summary>
    /// Current controller state.
    /// </summary>
    [SerializeField, Tooltip("Current state of the controller.")]
    private ControllerState currentState = ControllerState.Stopped;

    /// <summary>
    /// Is the state loaded?
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// Is the state active?
    /// </summary>
    public bool IsPlaying => currentState == ControllerState.Playing;

    /// <summary>
    /// Is the state stopped?
    /// </summary>
    public bool IsStopped => currentState == ControllerState.Stopped;

    /// <summary>
    /// Is the state paused?
    /// </summary>
    public bool IsPaused => currentState == ControllerState.Paused;

    #endregion

    #region Methods

    /// <summary>
    /// Check if a particular scene is already open.
    /// </summary>
    /// <param name="sceneName">Name of scene to check for.</param>
    /// <returns>Return true if loaded.</returns>
    public static bool IsSceneLoaded(string sceneName)
    {
        if(!string.IsNullOrEmpty(sceneName) && !string.IsNullOrWhiteSpace(sceneName))
        {
            // Get the current scene name.

        }

        // Scene doesn't exist or is not loaded.
        return false;
    }

    /// <summary>
    /// Change the state.
    /// </summary>
    /// <param name="state">State to change to.</param>
    private void ChangeState(ControllerState state) => this.currentState = state;

    /// <summary>
    /// Pause the state.
    /// </summary>
    protected virtual void Pause() => ChangeState(ControllerState.Paused);

    /// <summary>
    /// Play the state.
    /// </summary>
    protected virtual void Play() => ChangeState(ControllerState.Playing);

    /// <summary>
    /// Stop the state.
    /// </summary>
    protected virtual void Stop() => ChangeState(ControllerState.Stopped);

    #endregion

}
