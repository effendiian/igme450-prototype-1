using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Handles behaviour for the MainMenu UI.
/// </summary>
public class MainMenuController : MonoBehaviour
{

    /// <summary>
    /// Will help load the Instructions scene.
    /// </summary>
    public ModalLoader instructionsLoader;

    /// <summary>
    /// Collection of Buttons in the scene.
    /// </summary>
    public Button[] buttons;
    
    /// <summary>
    /// Start the game.
    /// </summary>
    public void StartGame()
    {
        // Pop the main menu state.
        Game.Instance.GameStateManager.PopState();

        // Load up the next scene.
        Game.Instance.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    } 
       
    /// <summary>
    /// Display the instructions.
    /// </summary>
    public void DisplayInstructions()
    {
        // Load the modal instructions.
        this.StartCoroutine(this.WaitForPanel());
    }

    /// <summary>
    /// Wrapper for the Quit function.
    /// </summary>
    public void Quit() => Game.Instance.Quit();

    /// <summary>
    /// On Awake, prep the instructions loader.
    /// </summary>
    public void Awake()
    {
        this.instructionsLoader = this.instructionsLoader ?? gameObject.GetComponent<ModalLoader>();
        this.buttons = this.buttons ?? FindObjectsOfType<Button>();
    }

    /// <summary>
    /// Set interactable flag for buttons in the scene.
    /// </summary>
    /// <param name="interactable">Value</param>
    public void SetInteractable(bool flag)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = flag;
        }
    }

    /// <summary>
    /// When the 
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitForPanel()
    {
        this.SetInteractable(false);
        yield return this.instructionsLoader.LoadSceneRoot();
        while (this.instructionsLoader.panelVisibility)
        {
            yield return null;
        }
        this.instructionsLoader.rootGameObjects[0].SetActive(false);
        this.SetInteractable(true);
    }
}
