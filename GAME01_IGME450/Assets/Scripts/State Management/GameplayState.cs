using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Gameplay state will display game mechanics and the like.
/// </summary>
public class GameplayState : GameState
{

    #region Fields

    /// <summary>
    /// Game UI.
    /// </summary>
    [SerializeField, Label("Game UI")]
    private Text gameUI = null;

    /// <summary>
    /// Unpaused time since start.
    /// </summary>
    [SerializeField, ReadOnly, Label("Total Unpaused Time Since Start")]
    private float timeSinceStart = 0;

    #endregion

    #region State Methods

    /// <summary>
    /// Ensure there is only one of this state.
    /// </summary>
    public void Awake()
    {
        GameplayState[] states = FindObjectsOfType<GameplayState>();
        if (states.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Show the game UI.
    /// </summary>
    /// <param name="stateManager">State manager.</param>
    public override void Init(StateManager stateManager)
    {
        base.Init(stateManager);
        this.gameUI.gameObject.SetActive(true);
        this.DisplayUI();
    }

    /// <summary>
    /// Hide the UI.
    /// </summary>
    public override void Cleanup()
    {
        base.Cleanup();
        this.HideUI();
        this.gameUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// Hide the UI when paused.
    /// </summary>
    public override void Pause()
    {
        base.Pause();
        this.HideUI();
    }

    /// <summary>
    /// Display the UI when resumed.
    /// </summary>
    public override void Resume()
    {
        base.Resume();
        this.DisplayUI();
    }

    /// <summary>
    /// Handle events if not paused.
    /// </summary>
    public override void HandleEvents()
    {
        base.HandleEvents();

        if (!this.IsPaused && this.manager.IsState(this))
        {
            // If space is detected, change scene to the GameplayState.
            if (Input.GetKeyDown(KeyCode.Return))
            {
                this.manager.PopState();
                return;
            }
        }
    }

    /// <summary>
    /// Update the gameplay state.
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (!this.IsPaused && this.manager.IsState(this))
        {
            this.timeSinceStart += Time.deltaTime;
            this.gameUI.text = $"Gameplay State: {this.timeSinceStart}";
        }
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Display the Game UI.
    /// </summary>
    public void DisplayUI()
    {
        if (this.gameUI != null)
        {
            this.gameUI.enabled = true;
        }
    }

    /// <summary>
    /// Hide the Game UI.
    /// </summary>
    public void HideUI()
    {
        if (this.gameUI != null)
        {
            this.gameUI.enabled = false;
        }
    }

    #endregion
       
}
