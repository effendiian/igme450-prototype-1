using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit this class to add interaction toggle functionality.
/// </summary>
public interface UIInteractable
{

    /// <summary>
    /// Enable group interactions.
    /// </summary>
    void EnableInteractions();

    /// <summary>
    /// Disable group interactions.
    /// </summary>
    void DisableInteractions();

    /// <summary>
    /// Set/unset interactable status.
    /// </summary>
    /// <param name="value"></param>
    void SetInteractable(bool value);

}
