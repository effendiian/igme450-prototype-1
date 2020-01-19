using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Introduction state that will keep the start menu displayed while active.
/// </summary>
public class IntroductionState : GameState
{
       
    #region Fields

    /// <summary>
    /// Start UI.
    /// </summary>
    [SerializeField, Required, Label("Start UI")]
    private Text startUI = null;

    /// <summary>
    /// Unpaused time since start.
    /// </summary>
    [SerializeField, ReadOnly, Label("Total Unpaused Time Since Start")]
    private float timeSinceStart = 0;

    /// <summary>
    /// Target state to switch to.
    /// </summary>
    private GameState gameplayState = null;

    #endregion

    #region State Methods

    /// <summary>
    /// Ensure there is only one of this state.
    /// </summary>
    public void Awake()
    {
        IntroductionState[] states = FindObjectsOfType<IntroductionState>();
        if(states.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Show the start UI.
    /// </summary>
    /// <param name="stateManager">State manager.</param>
    public override void Init(StateManager stateManager)
    {
        base.Init(stateManager);
        this.gameplayState = FindObjectOfType<GameplayState>();
        this.startUI.gameObject.SetActive(true);
        this.DisplayUI();
    }

    /// <summary>
    /// Hide the UI.
    /// </summary>
    public override void Cleanup()
    {
        base.Cleanup();
        this.HideUI();
        this.startUI.gameObject.SetActive(false);
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

        if (!this.IsPaused)
        {
            // If escape is pressed, exit the application.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                return;
            }

            // If space is detected, change scene to the GameplayState.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.manager.PushState(gameplayState);
                return;
            }
        }
    }

    /// <summary>
    /// Update the introduction state.
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (!this.IsPaused && this.manager.IsState(this))
        {
            this.timeSinceStart += Time.deltaTime;
            this.startUI.text = $"Start Menu State: {this.timeSinceStart}";
        }
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Display the Start UI.
    /// </summary>
    public void DisplayUI()
    {
        if(this.startUI != null){
            this.startUI.enabled = true;
        }
    }

    /// <summary>
    /// Hide the Start UI.
    /// </summary>
    public void HideUI()
    {
        if (this.startUI != null)
        {
            this.startUI.enabled = false;
        }
    }

    #endregion

}
