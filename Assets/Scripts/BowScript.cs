using System;
using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to arrow prefab
    public Transform spawnPoint; // Reference to spawn point transform
    public float maxPullForce; // Maximum force applied to the arrow
    private float _pullForce; // Current pull force
    private bool _isPulling; // Is the player currently pulling the bow?
    private Camera _mainCamera;
    public Slider chargeSlider;

    void Awake()
    {
        chargeSlider.value = 0;
        chargeSlider.maxValue = maxPullForce;
        chargeSlider.minValue = 0;
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var transform1 = transform;
        var position = transform1.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePosition.y - position.y, mousePosition.x - position.x) * Mathf.Rad2Deg);
        
        // Charging the bow
        if (Input.GetMouseButtonDown(0))
        {
            _isPulling = true;
            _pullForce = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isPulling = false;
            FireArrow();
        }

        if (_isPulling)
        {
            _pullForce = Mathf.Clamp(_pullForce + 20*Time.deltaTime, 0, maxPullForce); // Increase pull force over time
            chargeSlider.value = _pullForce;
        }
    }
    
    private void FireArrow()
    {
        if (!(_pullForce > 0)) return;
        var arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
        var rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * _pullForce, ForceMode2D.Impulse); // Apply force based on pull force
    }
}
