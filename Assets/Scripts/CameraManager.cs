using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _cameraSpeed = 1f;
    public void MoveCamToRoom(PlayerEnterEventArgs playerEnterEventArgs)
    {
        Vector3 newPosition = playerEnterEventArgs._cameraPosition;
        _camera?.transform.DOMove(new Vector3(newPosition.x, newPosition.y, -10), _cameraSpeed);
    }
}
