using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// InstructionsController keeps track of modal visibility.
/// </summary>
public class InstructionsController : ModalController
{

    /// <summary>
    /// Close panel button.
    /// </summary>
    public GameObject closePanelButton;

    /// <summary>
    /// Trigger the close panel button to fade in.
    /// </summary>
    public void Initialize()
    {
        // Only one exists on the instructions modal.
        this.closePanelButton = this.closePanelButton ?? FindObjectOfType<Button>().gameObject;

        // Make the button invisible.
        this.closePanelButton.GetComponent<ButtonFader>().SetProperty(0.0f);

        // Fade the button in.
        this.closePanelButton.GetComponent<UIAnimation>().PlayAnimationOnce();
    }


}
