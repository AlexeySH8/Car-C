using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    private float _speedRotation = 50;

    void Update()
    {
        if (!PlayerController.Instance.IsGameOver)
            RotateObject();
    }

    private void RotateObject()
    {
        var rotationVector = Vector3.down * _speedRotation * Time.deltaTime;
        transform.Rotate(rotationVector);
    }
}

