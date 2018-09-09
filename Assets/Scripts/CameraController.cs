using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerReference;

    private Vector3 offsetVector;

    private void Awake()
    {
        offsetVector = transform.position - playerReference.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerReference.transform.position + offsetVector;
    }
}
