using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Scene data.
/// </summary>
[CreateAssetMenu(fileName = "New Scene Reference", menuName = "Game/Scene Reference")]
public class SceneData : ScriptableObject
{

    /// <summary>
    /// Name of the <see cref="Scene"/>.
    /// </summary>
    [SerializeField, Tooltip("Name of scene to load.")]
    private string sceneName;

    /// <summary>
    /// Name of the <see cref="Scene"/>.
    /// </summary>
    public string SceneName
    {
        get => this.sceneName;
        private set => this.sceneName = value;
    }

    /// <summary>
    /// Check if scene name is valid.
    /// </summary>
    public bool HasSceneName => !string.IsNullOrEmpty(this.sceneName) && !string.IsNullOrWhiteSpace(this.sceneName);

}
