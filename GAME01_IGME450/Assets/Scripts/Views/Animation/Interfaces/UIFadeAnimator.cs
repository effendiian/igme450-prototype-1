using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

/// <summary>
/// Component for fading a UI target.
/// </summary>
public abstract class UIFadeAnimator<T> : MonoBehaviour
{

    #region Data Members

    /// <summary>
    /// Animation target.
    /// </summary>
    public abstract UIFadeTarget<T> Target { get; }

    /// <summary>
    /// The minimum alpha value.
    /// </summary>
    [SerializeField, Label("Minimum Alpha Value"), Range(0.0f, 1.0f)]
    private float minAlpha = 0.0f;

    /// <summary>
    /// The maximum alpha value.
    /// </summary>
    [SerializeField, Label("Maximum Alpha Value"), Range(0.0f, 1.0f)]
    private float maxAlpha = 1.0f;

    /// <summary>
    /// Animation duration in seconds.
    /// </summary>
    [SerializeField, Label("Duration (in Seconds)"), Range(0.0f, 10.0f)]
    private float duration = 1.0f;

    /// <summary>
    /// Show debug messages?
    /// </summary>
    [Tooltip("Show debug messages in the console?")]
    public bool showDebugMessages = false;

    /// <summary>
    /// Callbacks invoked on fade in start.
    /// </summary>
    [BoxGroup("Fade In Events"), SerializeField, Tooltip("Callbacks invoked on fade in start.")]
    private UnityEvent OnFadeInBegin;

    /// <summary>
    /// Callbacks invoked on fade in end.
    /// </summary>
    [BoxGroup("Fade In Events"), SerializeField, Tooltip("Callbacks invoked on fade in end.")]
    private UnityEvent OnFadeInComplete;

    /// <summary>
    /// Callbacks invoked on fade out start.
    /// </summary>
    [BoxGroup("Fade Out Events"), SerializeField, Tooltip("Callbacks invoked on fade out start.")]
    private UnityEvent OnFadeOutBegin;

    /// <summary>
    /// Callbacks invoked on fade out end.
    /// </summary>
    [BoxGroup("Fade Out Events"), SerializeField, Tooltip("Callbacks invoked on fade out end.")]
    private UnityEvent OnFadeOutComplete;
    
    /// <summary>
    /// Reference to the fade animation.
    /// </summary>
    private IEnumerator fadeAnimation;

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the members.
    /// </summary>
    public void Start()
    {
        // Prepare events.
        this.OnFadeInBegin = this.OnFadeInBegin ?? new UnityEvent();
        this.OnFadeInComplete = this.OnFadeInComplete ?? new UnityEvent();
        this.OnFadeOutBegin = this.OnFadeOutBegin ?? new UnityEvent();
        this.OnFadeOutComplete = this.OnFadeOutComplete ?? new UnityEvent();
    }

    /// <summary>
    /// Perform a hard set of the alpha.
    /// </summary>
    /// <param name="target">Target to modify.</param>
    /// <param name="alpha">Alpha value.</param>
    public void SetAlpha(UIFadeTarget<T> target, float alpha) => target.SetAlpha(alpha);

    /// <summary>
    /// Perform a fade in operation.
    /// </summary>
    [Button("Fade In")]
    public virtual void FadeIn() => this.Fade(this.Target, this.minAlpha, this.maxAlpha, this.duration, this.OnFadeInBegin, this.OnFadeInComplete);

    /// <summary>
    /// Perform a fade out operation.
    /// </summary>
    [Button("Fade Out")]
    public virtual void FadeOut() => this.Fade(this.Target, this.maxAlpha, this.minAlpha, this.duration, this.OnFadeOutBegin, this.OnFadeOutComplete);

    /// <summary>
    /// Perform a general fade animation.
    /// </summary>
    /// <param name="target">Target to animate.</param>
    /// <param name="startAlpha">Starting alpha value.</param>
    /// <param name="endAlpha">Ending alpha value.</param>
    /// <param name="duration">Duration of animation.</param>
    protected virtual void Fade(UIFadeTarget<T> target, float startAlpha, float endAlpha, float duration, UnityEvent onStart = null, UnityEvent onEnd = null)
    {
        // Stop animation if it is running.
        if(fadeAnimation != null)
        {
            this.StopCoroutine(this.fadeAnimation);
            this.fadeAnimation = null;
        }

        // Assign new fade animation for the coroutine and execute.
        this.fadeAnimation = FadeAlpha(target, startAlpha, endAlpha, duration, onStart, onEnd);
        this.StartCoroutine(this.fadeAnimation);
    }

    /// <summary>
    /// Perform a fade animation on a target opacity.
    /// </summary>
    /// <param name="target">Target to animate.</param>
    /// <param name="startAlpha">Starting alpha value.</param>
    /// <param name="endAlpha">Ending alpha value.</param>
    /// <param name="duration">Duration of animation.</param>
    /// <returns>Coroutine.</returns>
    public virtual IEnumerator FadeAlpha(UIFadeTarget<T> target, float startAlpha, float endAlpha, float duration, UnityEvent onStart = null, UnityEvent onEnd = null)
    {
        // Clamp values.
        float end = Mathf.Clamp(endAlpha, 0.0f, 1.0f);
        float start = Mathf.Clamp(startAlpha, 0.0f, 1.0f);

        // Set the starting alpha value.
        target.SetAlpha(start);

        // Invoke onStart callback.
        this.Invoke(onStart);

        // Run coroutine for fading alpha value.
        float elapsedTime = 0.0f;
        while(elapsedTime <= duration && duration != 0.0f)
        {
            // Calculate and update alpha.
            float t = (elapsedTime / duration);
            float currentAlpha = Mathf.Lerp(start, end, t);
            target.SetAlpha(currentAlpha);

            // Increment timer.
            elapsedTime += Time.deltaTime;

            // Wait until end of frame before looping.
            yield return new WaitForEndOfFrame();
        }

        // Set the ending alpha value.
        target.SetAlpha(end);

        // If true flag for showing debug messages.
        if (this.showDebugMessages)
        {
            Debug.Log($"[{this.name}] Fade animation for [{typeof(T)}: {target.name}] completed.");
        }

        // Invoke onEnd callback.
        this.Invoke(onEnd);
    }

    /// <summary>
    /// Invoke a supplied callback if it is non-null.
    /// </summary>
    /// <param name="callback">Callback to invoke.</param>
    private void Invoke(UnityEvent callback)
    {
        // If event is non-null, invoke it.
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    #endregion

}
