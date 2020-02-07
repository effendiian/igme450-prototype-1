using UnityEngine;

/// <summary>
/// Inherit from this base class to create a Singleton. Prevents memory leak from prior naive Singleton implementation.
/// Modified from: https:wiki.unity3d.com/index.php/Singleton
/// </summary>
/// <typeparam name="T">MonoBehaviour singleton.</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    #region Static Members

    /// <summary>
    /// Singleton is being shut down.
    /// </summary>
    private static bool s_Destroyed = false;

    /// <summary>
    /// Object reference for locking access across threads.
    /// </summary>
    private static object s_Lock = new object();

    /// <summary>
    /// Instance reference.
    /// </summary>
    private static T s_Instance;

    /// <summary>
    /// Property access to the Singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (s_Destroyed) 
            {
                Debug.LogWarning($"[Singleton] Instance ${typeof(T)} already destroyed. Returning null.");
                return null;
            }

            lock (s_Lock)
            {
                if(s_Instance == null)
                {
                    // Search for existing instance.
                    s_Instance = (T)FindObjectOfType(typeof(T));

                    // Create new instance if it doesn't exist.
                    if(s_Instance == null)
                    {
                        // Create new GameObject to attach Singleton to.
                        GameObject singletonObject = new GameObject($"'${typeof(T)}' (Singleton)");
                        s_Instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                // Return instance reference.
                return s_Instance;
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Set destroyed, when object is explicitly destroyed.
    /// </summary>
    protected virtual void OnDestroy() => s_Destroyed = true;

    /// <summary>
    /// Set destroyed, when application is quit.
    /// </summary>
    protected virtual void OnApplicationQuit() => s_Destroyed = true;

    #endregion

}
