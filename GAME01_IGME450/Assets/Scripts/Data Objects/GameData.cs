using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Data for the Game class.
/// </summary>
[System.Serializable]
public struct GameData
{
    /// <summary>
    /// Show debug messages in the Game class.
    /// </summary>
    [SerializeField, Label("Show Debug Messages?"), Tooltip("Show debug messages from the Game class.")]
    public bool showDebugMessages;
}