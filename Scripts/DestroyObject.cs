using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float _xBoundary = -160;
    private float _yBoundary = -40;

    void Update()
    {
        if (IsOutOfBounds())
            Destroy(gameObject);
    }

    private bool IsOutOfBounds() => transform.position.x < _xBoundary ||
            transform.position.y < _yBoundary;
}
