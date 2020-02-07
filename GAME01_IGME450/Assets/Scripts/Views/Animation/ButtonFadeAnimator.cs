using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Fade animator for UI elements.
/// </summary>
public class ButtonFadeAnimator : UIFadeAnimator<Button>
{
    
    /// <summary>
    /// Target that needs to be referenced.
    /// </summary>
    [Required, SerializeField, Label("UI Target")]
    private ButtonFadeTarget target;

    /// <summary>
    /// Override property reference.
    /// </summary>
    public override UIFadeTarget<Button> Target => this.target;

}
