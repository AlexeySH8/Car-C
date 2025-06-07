using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Update()
    {
        RotateWheel();
    }

    private void RotateWheel()
    {
        var rotationVector = Vector3.up * _speed * Time.deltaTime;
        transform.Rotate(rotationVector);
    }
}
