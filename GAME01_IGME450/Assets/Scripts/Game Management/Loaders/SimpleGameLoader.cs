using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

// Good reference: https://www.toptal.com/unity-unity3d/unity-with-mvc-how-to-level-up-your-game-development

/// <summary>
/// SimpleGameLoader used to setup Game with MainMenu.
/// </summary>
public class SimpleGameLoader : MonoBehaviour, IGameLoader
{

    #region Data Members

    /// <summary>
    /// Class settings.
    /// </summary>
    [SerializeField]
    private GameLoaderData _settings = Game.Instance.DefaultSettings.gameLoaderData;

    /// <summary>
    /// Property reference for <see cref="debug"/>.
    /// </summary>
    public bool ShowDebugMessages
    {
        get => this._settings.showDebugMessages;
        protected set => this._settings.showDebugMessages = value;
    }
    
    /// <summary>
    /// Property reference for <see cref="sceneName"/>.
    /// </summary>
    public string SceneName
    {
        get => this._settings.sceneName;
        set => this._settings.sceneName = value;
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Setup the game.
    /// </summary>
    public void Setup()
    {
        // Perform setup here.

        if (this.ShowDebugMessages)
        {
            Debug.Log("Setup completed successfully");
        }
    }

    /// <summary>
    /// Load new scene.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    public IEnumerator LoadProcess()
    { 
        if (this.ShowDebugMessages)
        {
            Debug.Log($"Loading [Scene {this.SceneName}]...");
        }

        // Wait two seconds.
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(this.SceneName, LoadSceneMode.Additive);

        if (this.ShowDebugMessages)
        {
            Debug.Log($"Loaded [Scene {this.SceneName}].");
        }
    }

    #endregion
    
}
