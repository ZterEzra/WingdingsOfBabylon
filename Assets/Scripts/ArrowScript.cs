using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); // Get Rigidbody on Start
    }

    void FixedUpdate()
    {
        // Get normalized direction vector
        Vector2 direction = _rigidBody.velocity.normalized;

        // Calculate angle in degrees using Atan2
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the arrow based on the angle
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
