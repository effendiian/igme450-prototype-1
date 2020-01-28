using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// ApplicationDataObject stores data and configuration settings that are important for the game.
/// </summary>
[CreateAssetMenu(fileName = "Application Data", menuName = "Application")]
public class ApplicationData : ScriptableObject
{

    #region Data Members
    
    /// <summary>
    /// Game class data.
    /// </summary>
    [Label("Game Data"), Tooltip("Data for the Game class.")]
    public GameData gameData;

    /// <summary>
    /// GameLoader data.
    /// </summary>
    [Label("GameLoader Data"), Tooltip("Data for IGameLoader implementations.")]
    public GameLoaderData gameLoaderData;

    #endregion

}
