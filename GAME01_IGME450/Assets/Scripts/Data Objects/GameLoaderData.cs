using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Data for the GameLoader class.
/// </summary>
[System.Serializable]
public struct GameLoaderData
{
    /// <summary>
    /// Show debug messages in the IGameLoader implementations.
    /// </summary>
    [SerializeField, Label("Show Debug Messages?"), Tooltip("Show debug messages from IGameLoader implementations.")]
    public bool showDebugMessages;

    /// <summary>
    /// Initial scene name for the IGameLoader implementations.
    /// </summary>
    [SerializeField, Label("Initial Scene"), Tooltip("Name of scene to load at game start.")]
    public string sceneName;
}
