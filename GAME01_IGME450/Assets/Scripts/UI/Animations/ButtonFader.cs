using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

/// <summary>
/// ButtonFader component is placed on a Button.
/// </summary>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(UIAnimation))]
public class ButtonFader : UIAnimationTarget
{

    #region Data Members
    
    /// <summary>
    /// Reference to the button background.
    /// </summary>
    [SerializeField, Label("Button Image"), Tooltip("Reference to button background.")]
    private Image _image;

    /// <summary>
    /// Access to the image.
    /// </summary>
    public Image Image => _image ?? (_image = gameObject.GetComponent<Image>());

    /// <summary>
    /// Reference to the button label.
    /// </summary>
    [SerializeField, Label("Button Text"), Tooltip("Reference to button label.")]
    private TextMeshProUGUI _text;

    /// <summary>
    /// Access to the text.
    /// </summary>
    public TextMeshProUGUI Text => _text ?? (_text = gameObject.GetComponentInChildren<TextMeshProUGUI>());

    #endregion
    
    #region UIAnimationTarget Methods

    /// <summary>
    /// Set the target value.
    /// </summary>
    /// <param name="value">Value to set.</param>
    public override void SetProperty(float value) => this.SetOpacity(value);

    #endregion

    #region Service Methods

    /// <summary>
    /// Set the opacity.
    /// </summary>
    /// <param name="opacity">Opacity of the element.</param>
    private void SetOpacity(float opacity)
    {
        // Get temporary color reference.
        Color tImageColor = this.Image.color;
        Color tTextColor = this.Text.color;

        // Assign opacity value.
        tImageColor.a = opacity;
        tTextColor.a = opacity;

        // Apply changes.
        this.Image.color = tImageColor;
        this.Text.color = tTextColor;
    }

    /// <summary>
    /// Disable the button when the animation starts.
    /// </summary>
    public void DisableButton() => this.gameObject.GetComponent<Button>().interactable = false;

    /// <summary>
    /// Enable the button after the animation ends.
    /// </summary>
    public void EnableButton() => this.gameObject.GetComponent<Button>().interactable = true;

    #endregion

}
