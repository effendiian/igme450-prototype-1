using UnityEngine;

/// <summary>
/// Set the animation target value.
/// </summary>
public abstract class UIAnimationTarget : MonoBehaviour
{

    /// <summary>
    /// Set the animation property value.
    /// </summary>
    /// <param name="value">Value to assign to the target property.</param>
    public abstract void SetProperty(float value);

}
