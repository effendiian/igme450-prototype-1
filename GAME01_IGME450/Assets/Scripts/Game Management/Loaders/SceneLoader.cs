using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// LoadSceneReceiver processes LoadSceneCommand objects.
/// </summary>
public class SceneLoader : MonoBehaviour
{

    #region Static Members

    /// <summary>
    /// SceneLoader singleton instance.
    /// </summary>
    private static SceneLoader _instance;

    /// <summary>
    /// Property reference to singleton instance.
    /// </summary>
    public static SceneLoader Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("Scene Loader");
                go.AddComponent<SceneLoader>();
            }
            return _instance;
        }
    }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Ensure only one exists.
    /// </summary>
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Load scene synchronously, additively or singularly.
    /// </summary>
    /// <param name="sceneName">Scene to load.</param>
    /// <param name="mode">Mode to load scene in.</param>
    /// <returns>Returns true if operation is successful.</returns>
    public bool LoadScene(string sceneName, LoadSceneMode mode)
    {
        // If scene doesn't exist at specified name, nothing will occur.
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null || !scene.IsValid())
        {
            return false;
        }

        // Load the scene.
        SceneManager.LoadScene(sceneName, mode);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        return true;
    }

    /// <summary>
    /// Load a scene additively or singularly.
    /// </summary>
    /// <param name="sceneName">Scene to load.</param>
    /// <param name="mode">Mode to load scene in.</param>
    public IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode mode)
    {
        // If scene doesn't exist at specified name, nothing will occur.
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene != null && scene.name != null && scene.IsValid())
        {
            yield break;
        }

        // Begin asyncrhonous operation.
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!operation.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    /// <summary>
    /// Load a scene additively or singularly.
    /// </summary>
    /// <param name="sceneName">Scene to load.</param>
    /// <param name="mode">Mode to load scene in.</param>
    public IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode mode, System.Action OnFailure, System.Action OnComplete)
    {
        // If scene doesn't exist at specified name, nothing will occur.
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null || !scene.IsValid())
        {
            OnFailure();
            yield break;
        }

        // Begin asyncrhonous operation.
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!operation.isDone)
        {
            yield return null;
        }

        OnComplete();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    #endregion

}
