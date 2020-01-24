using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// SceneComposers will pull together the different additive scenes necessary on Awake.
/// </summary>
public class SceneComposer : MonoBehaviour
{

    [SerializeField]
    private string sceneID = "";

    /// <summary>
    /// Initiate this state on the manager and then 
    /// </summary>
    public void Awake()
    {
        Game.Instance.LoadScene(this.sceneID);
    }
       
}
