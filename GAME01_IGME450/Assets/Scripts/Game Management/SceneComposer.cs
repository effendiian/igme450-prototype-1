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
    public virtual void Awake()
    {
        if (persist) { DontDestroyOnLoad(this); }
        this.loader = this.loader ?? gameObject.GetComponent<AdditiveSceneLoader>();
        this.loader.Setup();
    }
    
    /// <summary>
    /// Quit the game (through the Game manager).
    /// </summary>
    public void Quit() => Game.Instance.Quit();

}
