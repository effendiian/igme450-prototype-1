using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

/// <summary>
/// Gain access to another scene's root.
/// </summary>
public class ModalLoader : MonoBehaviour
{

    /// <summary>
    /// Name of the scene to load.
    /// </summary>
    public string sceneName = "";

    /// <summary>
    /// Scene Root will be populated once the other scene is loaded.
    /// </summary>
    public GameObject[] rootGameObjects = null;

    /// <summary>
    /// Panel visibility.
    /// </summary>
    public bool panelVisibility = true;

    /// <summary>
    /// This will load the scene root.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    public IEnumerator LoadSceneRoot()
    {
        // Close panel flag is reset.
        this.panelVisibility = true;

        if (rootGameObjects == null || rootGameObjects.Length == 0)
        {
            // Asynchronously load next scene.
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!ao.isDone)
            {
                yield return null;
            }

            // Get the loaded scene.
            Scene target = SceneManager.GetSceneByName(sceneName);
            rootGameObjects = target.GetRootGameObjects();

            // Yield break if still no root objects.
            if (rootGameObjects == null || rootGameObjects.Length == 0)
            {
                yield break;
            }
        }

        // Get the ModalController from the single root game object. (If hierarchy is correctly followed).
        rootGameObjects[0].SetActive(true);
        ModalController mc = rootGameObjects[0].GetComponent<ModalController>();
        mc.SetVisibility(true);
        yield return WatchSceneRoot(mc);
    }

    /// <summary>
    /// Watch the scene root until it is closed.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    public IEnumerator WatchSceneRoot(ModalController mc)
    {
        // This will wait until the panel visiblity is false.
        yield return new WaitUntil(() => !mc.GetVisibility());
        this.panelVisibility = mc.GetVisibility();
    }
}
