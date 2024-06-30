using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CinemachineTarget : MonoBehaviour
{
    private void Start()
    {
        if (PhotonNetwork.IsConnected && GetComponent<PhotonView>().IsMine)
        {
            SetCinemachineTarget();
        }
    }

    private void SetCinemachineTarget()
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
            //virtualCamera.LookAt = transform;
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera not found");
        }
    }
}
