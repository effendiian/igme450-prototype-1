using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Animation mode applied to a UI animation.
/// </summary>
public enum UIAnimationMode
{
    End,
    Wrap,
    PingPong
}

/// <summary>
/// Interface for UIAnimations.
/// </summary>
public interface IUIAnimation
{

    /// <summary>
    /// Set an animation mode to the UIAnimation.
    /// </summary>
    /// <param name="mode">Mode to assign.</param>
    void SetUIAnimationMode(UIAnimationMode mode);

    /// <summary>
    /// Set the animation duration (in seconds).
    /// </summary>
    /// <param name="duration">Duration of the animation (in seconds).</param>
    void SetDuration(float duration);

    /// <summary>
    /// Start the UIAnimation.
    /// </summary>
    /// <returns>Coroutine process.</returns>
    IEnumerator Animate();

}
