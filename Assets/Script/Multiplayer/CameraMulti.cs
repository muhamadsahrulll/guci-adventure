using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMulti : MonoBehaviour
{
    public Transform playerTransform; // Referensi ke transformasi pemain
    public BoxCollider2D cameraBounds; // Referensi ke collider batas kamera
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float halfHeight;
    private float halfWidth;

    private void Start()
    {
        if (cameraBounds != null)
        {
            minBounds = cameraBounds.bounds.min;
            maxBounds = cameraBounds.bounds.max;
        }

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position;
            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
