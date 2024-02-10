using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnterHandler : MonoBehaviour
{
    [Header("RoomArgs")]
    [SerializeField]
    private AudioClip _ambient;

    [Space(5)]

    [Header("Events")]
    public UnityEvent<PlayerEnterEventArgs> PlayerEnter;
    public UnityEvent PlayerOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player.PlayerController playerController))
        {
            PlayerEnter?.Invoke(new PlayerEnterEventArgs()
            {
                _cameraPosition = transform.position,
                _ambient = _ambient
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player.PlayerController playerController))
        {
            PlayerOut?.Invoke();
        }
    }
}
