using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetPos;
    public float lerpSpeed = 0.01f;

    void Start()
    {
        targetPos = new Vector3(0, 1, -9);
        transform.position = targetPos;
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
        transform.position = lerpedPosition;
    }

    public void MoveTo(Vector3 _newXPos)
    {
        targetPos = _newXPos;
    }


}
