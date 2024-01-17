using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referensi ke objek pemain
    public Transform kuda;
    public float smoothSpeed = 0.125f;  // Kecepatan pergerakan kamera

    private bool followTarget = true;  // Gunakan ini untuk menentukan apakah akan mengikuti target atau kuda

    void LateUpdate()
    {
        if (followTarget && target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else if (!followTarget && kuda != null)
        {
            Vector3 desiredPosition = new Vector3(kuda.position.x, kuda.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    // Method untuk mengubah target yang diikuti (target atau kuda)
    public void SetFollowTarget(bool followTarget)
    {
        this.followTarget = followTarget;
    }
    public bool GetFollowTarget()
    {
        return followTarget;
    }
}
