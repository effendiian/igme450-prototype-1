using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// GameManager for managing game state within and across scenes.
/// </summary>
public class GameManager : StateManager
{

    #region Fields

    /// <summary>
    /// Entry state for this manager.
    /// </summary>
    [SerializeField]
    private GameState entry;

    #endregion

    #region Manager Methods

    /// <summary>
    /// Ensure only one instance of the GameManager exists.
    /// </summary>
    public void Awake()
    {
        GameManager[] managers = FindObjectsOfType<GameManager>();
        if (managers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        entry = FindObjectOfType<IntroductionState>();
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    #region Service Methods

    /// <summary>
    /// Start the entry point state.
    /// </summary>
    public override void Start()
    {
        base.Start();
        
        // Create the introduction state.
        if(entry == null)
        {
            // Instantiate empty child object.
            GameObject intro = new GameObject("IntroductionState");
            intro.AddComponent<IntroductionState>();
        }

        // Push and initialize the state.
        this.PushState(entry);
    }
    
    #endregion

}
