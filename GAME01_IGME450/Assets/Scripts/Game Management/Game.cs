using UnityEngine;
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
    public static Game Instance => Game._instance;

    #endregion

    #region Data Members

    /// <summary>
    /// ApplicationData reference.
    /// </summary>
    [SerializeField, Label("Default Application Data"), Tooltip("Application Data asset reference.")]
    private ApplicationData _applicationData;

    /// <summary>
    /// Property reference to the application data.
    /// </summary>
    public ApplicationData DefaultSettings => _applicationData ?? (_applicationData = ScriptableObject.CreateInstance<ApplicationData>());
    
    /// <summary>
    /// Loader for the game.
    /// </summary>
    [SerializeField, ReadOnly, Label("Game Loader"), Tooltip("Game Loader behaviour.")]
    private IGameLoader _loader;

    /// <summary>
    /// Reference to the Game Loader.
    /// </summary>
    public IGameLoader Loader => _loader ?? (_loader = gameObject.AddComponent<SimpleGameLoader>());

    #endregion

    #region MonoBehaviours

    // Don't destroy this on load and ensure there is only one instance.
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

            // Setup the game using the loader.
            this.Loader.Setup();

            // Call the loader coroutine.
            this.StartCoroutine(this.Loader.LoadProcess());
        }
    }

    #endregion
    
}
