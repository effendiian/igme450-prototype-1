using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// This base game course is modified and based off of the 'Single-entry point'
// tutorial given by Ruben Torres Bonet on Gamasutra source:
// https://www.gamasutra.com/blogs/RubenTorresBonet/20180703/316442/A_better_architecture_for_Unity_projects.php

/// <summary>
/// Abstract class that should be implemented by Game launchers.
/// </summary>
public abstract class BaseGame : MonoBehaviour
{
        
    /// <summary>
    /// Don't destroy the launcher on load and perform initial setup.
    /// </summary>
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        this.SetupGame();
    }

    /// <summary>
    /// Initialize the game and appropriate dependencies.
    /// </summary>
    protected virtual void SetupGame()
    {
        // Good reference: https://www.toptal.com/unity-unity3d/unity-with-mvc-how-to-level-up-your-game-development

        // TODO: Implement COMMAND interface.
        // https://stackoverflow.com/questions/8788366/using-the-command-and-factory-design-patterns-for-executing-queued-jobs

        // Load dependencies synchronously.
        this.LoadDependencies();

        // Then, the initial scene is loaded.
        this.StartCoroutine(this.LoadProcess());
    }

    /// <summary>
    /// Dependencies are loaded here.
    /// </summary>
    /// <returns></returns>
    protected abstract void LoadDependencies();

    /// <summary>
    /// Load process coroutine that is started after game setup has resolved.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    protected abstract IEnumerator LoadProcess();



}
