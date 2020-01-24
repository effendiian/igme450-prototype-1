using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// SceneComposers will pull together the different additive scenes necessary on Awake.
/// </summary>
public class SceneComposer : MonoBehaviour
{

    /// <summary>
    /// Initial loader for the SceneComposer.
    /// </summary>
    public AdditiveSceneLoader loader;

    /// <summary>
    /// Don't destroy on load?
    /// </summary>
    [Label("Persist?"), Tooltip("Don't destroy this object on load?")]
    public bool persist = false;

    /// <summary>
    /// On Awake, assign the loader.
    /// </summary>
    public void Awake()
    {
        this.loader = this.loader ?? gameObject.GetComponent<AdditiveSceneLoader>();
        if (persist) { DontDestroyOnLoad(this); }
    }

    /// <summary>
    /// On Start, execute the loader.
    /// </summary>
    public void Start() => this.loader.Setup();

}
