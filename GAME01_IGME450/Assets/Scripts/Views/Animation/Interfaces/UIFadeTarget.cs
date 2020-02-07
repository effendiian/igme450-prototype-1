using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit this class to make the alpha value animatable.
/// </summary>
public abstract class UIFadeTarget<T> : MonoBehaviour
{

    /// <summary>
    /// Reference to the target this refers to.
    /// </summary>
    [SerializeField]
    protected T target;

    /// <summary>
    /// Show debug messages?
    /// </summary>
    [Tooltip("Show debug messages in the console?")]
    public bool showDebugMessages = false;

    /// <summary>
    /// Set the alpha value.
    /// </summary>
    /// <param name="alpha">Value to set.</param>
    public void SetAlpha(float alpha)
    {
        if (this.showDebugMessages)
        {
            Debug.Log($"[{typeof(T).ToString()}: {this.name}] alpha set to '{alpha}'.");
        }
        this.UpdateTarget(alpha);
    }

    /// <summary>
    /// Assign alpha value to the actual target.
    /// </summary>
    /// <param name="target">Target to update.</param>
    /// <param name="alpha">Alpha to assign.s</param>
    protected abstract void UpdateTarget(float alpha);

}
