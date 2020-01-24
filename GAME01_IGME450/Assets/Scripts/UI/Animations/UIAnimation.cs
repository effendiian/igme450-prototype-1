using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NaughtyAttributes;

/// <summary>
/// ButtonFadeAnimation will fade a button in or out, depending on the settings.
/// </summary>
public class UIAnimation : MonoBehaviour, IUIAnimation
{

    #region Data Members

    /// <summary>
    /// Target that is animated.
    /// </summary>
    [SerializeField, Label("Animation Target")]
    private UIAnimationTarget _target;
    private UIAnimationTarget Target => _target ?? (_target = gameObject.GetComponent<UIAnimationTarget>());

    /// <summary>
    /// Animation mode to apply.
    /// </summary>
    [SerializeField, Label("UI Animation Mode"), Tooltip("Controls animation post- and pre-Wrap behavior.")]
    private UIAnimationMode _animationMode = UIAnimationMode.End;
    
    /// <summary>
    /// Current UI animation property value.
    /// </summary>
    [SerializeField, ReadOnly, Label("Current Property Value")]
    private float _property = 0.0f;

    /// <summary>
    /// Target value scaling, when being set.
    /// </summary>
    [SerializeField, Label("Scaling"), Tooltip("Multiplier to apply to the animation values.")]
    private float _scale = 1.0f;

    /// <summary>
    /// Starting property value that will be used when animation is started.
    /// </summary>
    [SerializeField, Label("Start Value"), Range(0.0f, 1.0f)]
    private float _startValue = 0.0f;

    /// <summary>
    /// Target property value that will be reached when animation is completed.
    /// </summary>
    [SerializeField, Label("End Value"), Range(0.0f, 1.0f)]
    private float _endValue = 1.0f;

    /// <summary>
    /// Duration of time for the animation.
    /// </summary>
    [SerializeField, Label("Duration"), Tooltip("Duration of the animation (in seconds)."), Range(0.0f, 10.0f)]
    private float _duration = 1.0f;

    /// <summary>
    /// Animation curve used for the fade animation.
    /// </summary>
    [SerializeField, Label("Animation Curve")]
    private AnimationCurve _curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

    /// <summary>
    /// Animate on start.
    /// </summary>
    [SerializeField, Label("Animation On Play"), Tooltip("Start the animation on MonoBehaviour start.")]
    public bool animateOnStart = false;

    /// <summary>
    /// Reverse the animation.
    /// </summary>
    [SerializeField, Label("Reverse Animation"), Tooltip("Reverse the animation. Uses the 'start' value instead of the 'end' value.")]
    public bool reverse = false;

    /// <summary>
    /// Trigger event when animation starts.
    /// </summary>
    [SerializeField]
    public UnityEvent OnStart;

    /// <summary>
    /// Trigger event when animation ends.
    /// </summary>
    [SerializeField]
    public UnityEvent OnEnd;

    #endregion

    #region MonoBehaviour Methods 

    /// <summary>
    /// If animateOnStart is set to true, and target is non-null, animate.
    /// </summary>
    public void Start()
    {
        // Setup events.
        this.OnStart = this.OnStart ?? new UnityEvent();
        this.OnEnd = this.OnEnd ?? new UnityEvent();

        if(this.Target != null && this.animateOnStart)
        {
            this.StartCoroutine(this.Animate());
        }        
    }
    
    /// <summary>
    /// Validate method.
    /// </summary>
    public void OnValidate()
    {
        this.ValidateUIAnimationMode();
        this.SetDuration(this._duration);
    }

    #endregion

    #region IUIAnimation Methods

    /// <summary>
    /// Set the animation mode.
    /// </summary>
    /// <param name="mode">Animation mode.</param>
    public void SetUIAnimationMode(UIAnimationMode mode)
    {
        this._animationMode = mode;
        switch (_animationMode)
        {
            case UIAnimationMode.End:
                this.SetWrapModes(WrapMode.Clamp, WrapMode.Clamp);
                break;
            case UIAnimationMode.PingPong:
                this.SetWrapModes(WrapMode.PingPong, WrapMode.PingPong);
                break;
            case UIAnimationMode.Wrap:
                this.SetWrapModes(WrapMode.Loop, WrapMode.Loop);
                break;
            default:
                this.SetWrapModes(WrapMode.Clamp, WrapMode.Clamp);
                break;
        }
    }

    /// <summary>
    /// Set the duration of the animation (in seconds).
    /// </summary>
    /// <param name="duration">Duration of the animation (in seconds).</param>
    public void SetDuration(float duration) => this._duration = (duration < 0.0f) ? (0.0f) : (duration);

    /// <summary>
    /// Run the animation.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Animate()
    {
        // Prepare animation loop.
        float elapsedTime = 0.0f; // in seconds.  

        // Cache animation curve for this cycle.
        AnimationCurve curve = new AnimationCurve(this._curve.keys);

        // Assign proper start keyframe.
        Keyframe start = curve[0];
        start.time = 0.0f;
        start.value = this._startValue;
        curve.keys[0] = start;

        // Assign proper end keyframe.
        Keyframe end = this._curve[1];
        end.time = 1.0f;
        end.value = this._endValue;
        curve.keys[this._curve.keys.Length - 1] = end;

        // Invoke animation OnStart event.
        this.OnStart.Invoke();
        
        // Begin the animation.
        while (elapsedTime < this._duration || (this._animationMode == UIAnimationMode.PingPong || this._animationMode == UIAnimationMode.Wrap))
        {
            // Evaluate the animation.
            float t = elapsedTime / this._duration;

            // If reverse is true, we'll subtract this value from 1.0f.
            t = (reverse) ? (1.0f - t) : t;

            // Set the property value to the evaluated curve.
            this.SetProperty(this._curve.Evaluate(t));

            // Increment the elapsed time since last frame.
            elapsedTime += Time.deltaTime;
            
            // Yield animation.
            yield return null;
        }

        // When end, set the appropriate value. If reverse, use the start.
        this.SetProperty((this.reverse) ? this._startValue : this._endValue);

        // Invoke animation OnEnd event.
        this.OnEnd.Invoke();
    }

    #endregion
    
    #region Service Methods

    /// <summary>
    /// Play the UIAnimation once.
    /// </summary>
    [Button("Play Once")]
    public void PlayAnimationOnce()
    {
        this.SetUIAnimationMode(UIAnimationMode.End);
        this.SetProperty(0.0f);
        this.StartCoroutine(this.Animate());
    }

    /// <summary>
    /// Play the UIAnimation, repeating it from the beginning.
    /// </summary>
    [Button("Loop Animation")]
    public void PlayLoop()
    {
        this.SetUIAnimationMode(UIAnimationMode.Wrap);
        this.SetProperty(0.0f);
        this.StartCoroutine(this.Animate());
    }

    /// <summary>
    /// Play the UIAnimation, pingponging between the start and end.
    /// </summary>
    [Button("Play Ping-Pong")]
    public void PlayPingPong()
    {
        this.SetUIAnimationMode(UIAnimationMode.PingPong);
        this.SetProperty(0.0f);
        this.StartCoroutine(this.Animate());
    }
           
    /// <summary>
    /// Stop animation and jump to the target value.
    /// </summary>
    [Button("Stop Animation")]
    public void StopAnimation()
    {
        this.StopAllCoroutines();
        this.SetProperty(this._endValue);
        this.OnEnd.Invoke();
    }
    
    /// <summary>
    /// Set the current property value.
    /// </summary>
    /// <param name="value">Property value.</param>
    private void SetProperty(float value)
    {
        this._property = Mathf.Clamp(value, this._startValue, this._endValue) * this._scale;
        this.Target.SetProperty(this._property);
    }

    /// <summary>
    /// If value is changed in the inspector.
    /// </summary>
    public virtual void ValidatePropertyValues()
    {
        // Clamp the start and end values between zero and one.
        this._startValue = Mathf.Clamp(this._startValue, 0.0f, 1.0f);
        this._endValue = Mathf.Clamp(this._endValue, 0.0f, 1.0f);
    }
    
    /// <summary>
    /// Update animation mode changes, just incase it was changed in the inspector.
    /// </summary>
    public void ValidateUIAnimationMode() => this.SetUIAnimationMode(this._animationMode);

    /// <summary>
    /// Set wrap modes.
    /// </summary>
    /// <param name="preWrapMode">Animation curve's pre-wrap mode.</param>
    /// <param name="postWrapMode">Animation curve's post-wrap mode.</param>
    private void SetWrapModes(WrapMode preWrapMode, WrapMode postWrapMode)
    {
        this._curve.preWrapMode = preWrapMode;
        this._curve.postWrapMode = postWrapMode;
    }

    #endregion

}