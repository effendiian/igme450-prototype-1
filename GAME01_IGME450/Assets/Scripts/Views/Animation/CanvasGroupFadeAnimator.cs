using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CanvasGroupFadeAnimator : UIFadeAnimator<CanvasGroup>
{

    /// <summary>
    /// Target that needs to be referenced.
    /// </summary>
    [Required, SerializeField, Label("UI Target")]
    private CanvasGroupFadeTarget target;

    /// <summary>
    /// Override property reference.
    /// </summary>
    public override UIFadeTarget<CanvasGroup> Target => this.target;
}
