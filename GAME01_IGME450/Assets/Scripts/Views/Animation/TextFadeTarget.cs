using UnityEngine;
using TMPro;

/// <summary>
/// Fade animation for TextMeshProUGUI component.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFadeTarget : UIFadeTarget<TextMeshProUGUI>
{
    /// <summary>
    /// Initialize references on awake.
    /// </summary>
    void Awake()
    {
        // Get reference to the target.
        this.target = this.target ?? this.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Assign alpha value to the actual target.
    /// </summary>
    /// <param name="target">Target to update.</param>
    /// <param name="alpha">Alpha to assign.s</param>
    protected override void UpdateTarget(float alpha)
    {
        if (this.target != null)
        {
            Color col = this.target.color;
            col.a = alpha;
            this.target.color = col;
        }
    }
    
}
