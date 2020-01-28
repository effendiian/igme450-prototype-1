using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for loading game resources. Allows for specialized versions to be swapped out.
/// </summary>
public interface IGameLoader
{

    /// <summary>
    /// Setup the game.
    /// </summary>
    void Setup();

    /// <summary>
    /// Load process will trigger the loading of a scene.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    IEnumerator LoadProcess();

}
