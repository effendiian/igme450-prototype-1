using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MainMenuController : BaseController
{

    #region Data Members
    
    /// <summary>
    /// Scene containing the Main Menu UI.
    /// </summary>
    [Required, Label("Main Menu UI")]
    public SceneData ui_MainMenu;

    /// <summary>
    /// Scene containing the Instructions UI.
    /// </summary>
    [Required, Label("Instructions UI")]
    public SceneData ui_Instructions;
       
    #endregion

    #region Methods

    /// <summary>
    /// Prepare the scene.
    /// </summary>
    private void Awake()
    {
        // Not yet loaded.
        this.IsLoaded = false;
    }

    /// <summary>
    /// Load the UI for the main menu.
    /// </summary>
    private void Setup()
    {
        // Ensure the state is stopped.
        this.Stop();


    }

    #endregion


}
