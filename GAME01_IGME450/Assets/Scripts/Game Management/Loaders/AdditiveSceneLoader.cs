using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

/// <summary>
/// Abstract class for AdditiveSceneLoaders.
/// </summary>
public class AdditiveSceneLoader : MonoBehaviour, IGameLoader
{

    #region Data Members
    
    /// <summary>
    /// Name of scene to additively load.
    /// </summary>
    [SerializeField, Label("Scene Name"), Tooltip("Name of Scene to additively load.")]
    private string _sceneName = "";

    /// <summary>
    /// Merge the additively loaded scene into the current scene?
    /// </summary>
    [SerializeField, Label("Merge?"), Tooltip("Merge the new scene into the existing one?")]
    private bool _mergeScene = false;

    /// <summary>
    /// Event invoked when scene has an error.
    /// </summary>
    public UnityEvent OnError;

    /// <summary>
    /// Event invoked when scene is loaded.
    /// </summary>
    public UnityEvent OnLoaded;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Initialize Unity events.
    /// </summary>
    public void Start()
    {
        this.OnError = this.OnError ?? new UnityEvent();
        this.OnLoaded = this.OnLoaded ?? new UnityEvent();
    }

    #endregion

    #region IGameLoader Methods

    /// <summary>
    /// Perform setup prior to loading the scene.
    /// </summary>
    public virtual void Setup()
    {
        Debug.Log($"Checking if [Scene {this._sceneName}] exists...");
        if (!this.SceneExists(this._sceneName))
        {
            Debug.LogError($"[Scene {this._sceneName}] does not exist or cannot be loaded.");
            this.OnError.Invoke();
            return;
        }

        // If Setup is successful, load the scene additively.
        this.StartCoroutine(this.LoadProcess());
    }

    /// <summary>
    /// Load the specified scene additively.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    public virtual IEnumerator LoadProcess()
    {
        Debug.Log($"Additively loading [Scene {this._sceneName}]...");

        // Check if scene is already loaded.
        if (!Game.IsSceneLoaded(this._sceneName))
        {
            // Loading the scene additively.
            AsyncOperation ao = SceneManager.LoadSceneAsync(this._sceneName, LoadSceneMode.Additive);
            ao.allowSceneActivation = false;

            while (!ao.isDone)
            {
                Debug.Log($"Loading [Scene {this._sceneName}] progress: {ao.progress * 100}%");
                if (ao.progress >= 0.9f)
                {
                    // Activate the scene.
                    ao.allowSceneActivation = true;
                }
                yield return null;
            }
        }
        else
        {
            Debug.Log($"[Scene {this._sceneName}] already loaded!");
        }

        // Merge scenes if necessary.
        if (this._mergeScene)
        {
            this.Merge();
        }

        Debug.Log($"Load of [Scene {this._sceneName}] complete.");
        this.OnLoaded.Invoke();
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Check Scene exists.
    /// </summary>
    /// <param name="name">Name of Scene to check.</param>
    /// <returns>Returns true if loadable.</returns>
    public bool SceneExists(string name) => (name != "" && Application.CanStreamedLevelBeLoaded(name));

    /// <summary>
    /// Merge scene into active scene. Assumes Scene Name is valid.
    /// </summary>
    private void Merge()
    {
        Scene active = SceneManager.GetActiveScene();
        Scene target = SceneManager.GetSceneByName(this._sceneName);

        // If target scene is null, do not merge anything. It isn't loaded.
        if(target == null)
        {
            Debug.LogError($"Cannot find a loaded [Scene {this._sceneName}] by name.");
            this.OnError.Invoke();
            return;
        }

        // If the target scene is loaded and different from the active scene.
        if(active != null && active != target)
        {
            Debug.Log($"Merging [Scene {target.name} (Target)] into [Scene {active.name} (Active)].");
            SceneManager.MergeScenes(target, active);
        }
    }

    #endregion

}
