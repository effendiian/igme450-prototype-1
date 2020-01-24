using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// The MainMenuState is aware of the MainMenu.
/// </summary>
public class MainMenuState : GameState
{

    #region Data Members

    /// <summary>
    /// Reference to next state that the game will enter.
    /// </summary>
    private GameState _nextState = null;

    /// <summary>
    /// Reference to the controller.
    /// </summary>
    private MainMenuController _controller = null;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// On Awake, push this as the current state.
    /// </summary>
    public void Awake()
    {
        this._controller = gameObject.GetComponent<MainMenuController>();
        this._nextState = null;
        Game.Instance.GameStateManager.PushState(this);
    }

    #endregion

    #region GameState Methods

    /// <summary>
    /// Show the game UI.
    /// </summary>
    /// <param name="stateManager">State manager.</param>
    public override void Init(StateManager stateManager)
    {
        base.Init(stateManager);
        this.UnlockUI();
    }

    /// <summary>
    /// Hide the UI.
    /// </summary>
    public override void Cleanup()
    {
        base.Cleanup();
        this.LockUI();
    }

    /// <summary>
    /// Lock the UI when paused.
    /// </summary>
    public override void Pause()
    {
        base.Pause();
        this.LockUI();
    }

    /// <summary>
    /// Unlock the UI when resumed.
    /// </summary>
    public override void Resume()
    {
        base.Resume();
        this.UnlockUI();
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Lock UI interactions.
    /// </summary>
    public void LockUI()
    {
        this._controller.SetInteractable(false);
    }

    /// <summary>
    /// Unlock UI.
    /// </summary>
    public void UnlockUI()
    {
        this._controller.SetInteractable(true);
    }

    #endregion

}
