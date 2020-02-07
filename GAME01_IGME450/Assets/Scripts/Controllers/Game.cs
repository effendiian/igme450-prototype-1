using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Shared reference and functionality across application.
/// </summary>
public class Game : Singleton<Game>
{

    #region Static Members

    /// <summary>
    /// Reference to the current active scene.
    /// </summary>
    public Scene ActiveScene => SceneManager.GetActiveScene();

    /// <summary>
    /// Assign the time scale. (0.0f to pause, 1.0f to resume).
    /// </summary>
    public void SetTimeScale(float scale) => Time.timeScale = scale;

    #endregion

    #region Methods

    /// <summary>
    /// Quit the application.
    /// </summary>
    public static void Quit()
    {
#if UNITY_EDITOR
        // Exit the application. 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application.
        Application.Quit();
#endif
    }

    public bool HasScene(string )

    #endregion

}
