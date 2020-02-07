using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

/// <summary>
/// Scene controller with modal capabilities.
/// </summary>
public abstract class ModalController : MonoBehaviour, IModal
{

    #region Data Members

    /// <summary>
    /// Modal is visible when created, by default.
    /// </summary>
    [SerializeField, ReadOnly, Label("Is Visible?")]
    private bool _visible = true;

    /// <summary>
    /// When shown, this event will be invoked.
    /// </summary>
    public UnityEvent OnShow;

    /// <summary>
    /// When hidden/closed this event will be invoked.
    /// </summary>
    public UnityEvent OnHide;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Invoke event preparation.
    /// </summary>
    public void Start()
    {
        this.OnShow = this.OnShow ?? new UnityEvent();
        this.OnHide = this.OnHide ?? new UnityEvent();
    }

    #endregion

    #region IModal Methods

    /// <summary>
    /// Return current visibility flag.
    /// </summary>
    /// <returns>Returns value of visibility flag.</returns>
    public bool GetVisibility() => this._visible;

    /// <summary>
    /// Set the visibility of the modal.
    /// </summary>
    /// <param name="flag">Value of visibility flag.</param>
    public void SetVisibility(bool flag)
    {
        if (flag)
        {
            this._visible = true;
            this.OnShow.Invoke();
        }
        else
        {
            this._visible = false;
            this.OnHide.Invoke();
        }
    }

    #endregion

}
