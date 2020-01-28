/// <summary>
/// Get the panel status for a modal scene.
/// </summary>
public interface IModal
{

    /// <summary>
    /// Get the Modal's panel visibility status.
    /// </summary>
    /// <returns>Returns false if needs to close.</returns>
    bool GetVisibility();

    /// <summary>
    /// Set the Modal's panel visibility status.
    /// </summary>
    /// <param name="flag">Flag value to assign.</param>
    void SetVisibility(bool flag);
}