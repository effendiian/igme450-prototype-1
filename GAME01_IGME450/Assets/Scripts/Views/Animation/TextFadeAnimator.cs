using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class TextFadeAnimator : UIFadeAnimator<TextMeshProUGUI>
{

    /// <summary>
    /// Target that needs to be referenced.
    /// </summary>
    [Required, SerializeField, Label("UI Target")]
    private TextFadeTarget target;

    /// <summary>
    /// Override property reference.
    /// </summary>
    public override UIFadeTarget<TextMeshProUGUI> Target => this.target;

}
