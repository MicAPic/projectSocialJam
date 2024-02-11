using DG.Tweening;
using Rooms;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField]
        private float _cameraSpeed = 1f;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void MoveCamToRoom(PlayerEnterEventArgs playerEnterEventArgs)
        {
            Vector3 newPosition = playerEnterEventArgs.cameraPosition;
            _camera.transform.DOMove(new Vector3(newPosition.x, newPosition.y, -10), _cameraSpeed);
        }
    }
}
