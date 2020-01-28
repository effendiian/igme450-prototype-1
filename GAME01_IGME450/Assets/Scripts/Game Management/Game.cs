using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

/***
 * This is a modified Game manager 
 * based off of the 'Single-entry point'
 * tutorial given by Ruben Torres Bonet
 * on Gamasutra source: https://www.gamasutra.com/blogs/RubenTorresBonet/20180703/316442/A_better_architecture_for_Unity_projects.php
 ***/

/// <summary>
/// Entry point for the game.
/// </summary>
public class Game : MonoBehaviour
{

    #region Static Members

    /// <summary>
    /// Instance of game is null when first initialized.
    /// </summary>
    private static Game _instance;

    /// <summary>
    /// Singleton reference to <see cref="Game"/> class.
    /// </summary>
    public static Game Instance
    {
        get
        {
            if(Game._instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                Game._instance = go.AddComponent<Game>();
            }

            return Game._instance;
        }
    }

    /// <summary>
    /// Check if scene of a particular name is currently loaded.
    /// </summary>
    /// <param name="sceneName">Name of scene to check.</param>
    /// <returns>Returns true if found.</returns>
    public static bool IsSceneLoaded(string sceneName)
    {
#if UNITY_EDITOR
        for(int i=0; i < UnityEditor.SceneManagement.EditorSceneManager.loadedSceneCount; ++i)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);
            if(scene.name == sceneName)
            {
                return true;
            }
        }
#else     
        for(int i=0; i < SceneManager.sceneCount; ++i)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.name == sceneName)
            {
                return true;
            }
        }
#endif
        return false;
    }

    #endregion

    #region Data Members

    /// <summary>
    /// Map of scene names.
    /// </summary>
    private Dictionary<string, string> _scenes;

    /// <summary>
    /// Global level state manager.
    /// </summary>
    private StateManager _globalStateManager;

    /// <summary>
    /// Global State Manager.
    /// </summary>
    public StateManager GameStateManager => this._globalStateManager;

    #endregion

    #region MonoBehaviours

    /// <summary>
    /// Don't destroy this on load and ensure there is only one instance.
    /// </summary>
    protected virtual void Awake()
    {
        // Determine if this Singleton has already been initialized.
        if(_instance != null && _instance != this)
        {
            // Remove this Component.
            Destroy(this);
        }
        else
        {
            // Assign instance.
            _instance = this;

            // For the Game controller, ensure it is persistent between scenes.
            DontDestroyOnLoad(this);

            // Create collection for scenes.
            this.Setup();

            // Load a scene.
            this.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }

    /// <summary>
    /// On application exit, perform the following.
    /// </summary>
    public void OnApplicationQuit()
    {
        Debug.Log("Exiting the application.");
        Destroy(this);
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Setup the Game (and its map key).
    /// </summary>
    public void Setup()
    {
        // Setup Scene map.
        this._scenes = this._scenes ?? new Dictionary<string, string>();
        this._scenes.Add("Game", "scn_LikeButtonPrototype");
        this._scenes.Add("Main Menu", "scn_MainMenu");
        this._scenes.Add("Main Menu UI", "scn_MainMenuUI");
        this._scenes.Add("Instructions UI", "scn_InstructionsUI");

        // Setup state manager.
        this._globalStateManager = this._globalStateManager ?? gameObject.AddComponent<StateManager>();

        // Setup scene loader.
        this.gameObject.AddComponent<SceneLoader>();
    }

    /// <summary>
    /// Load Scene by mapped ID key.
    /// </summary>
    /// <param name="id">ID key mapped to a scene name.</param>
    /// <param name="mode">Mode to load scene with.</param>
    public void LoadScene(string id, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        if (this._scenes.ContainsKey(id))
        {
            this.StartCoroutine(SceneLoader.Instance.LoadSceneAsync(this._scenes[id], mode));
        }
    }

    /// <summary>
    /// Quit the Application.
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion

}
