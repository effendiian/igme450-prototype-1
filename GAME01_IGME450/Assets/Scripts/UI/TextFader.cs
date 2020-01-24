using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

/// <summary>
/// ButtonFader component is placed on a Button.
/// </summary>
[RequireComponent(typeof(UIAnimation))]
public class TextFader : UIAnimationTarget
{

    #region Data Members

    /// <summary>
    /// Reference to the Logo text.
    /// </summary>
    [SerializeField, Label("Logo Text"), Tooltip("Reference to logo text.")]
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
        Color tTextColor = this.Text.color;

        // Assign opacity value.
        tTextColor.a = opacity;

        // Apply changes.
        this.Text.color = tTextColor;
    }

    #endregion

}
