using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rooms;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private RoomEnterHandler _livingRoom;
    [SerializeField]
    private RoomEnterHandler _basementRoom;
    [SerializeField]
    private RoomEnterHandler _garageRoom;
    [SerializeField]
    private RoomEnterHandler _parentsRoom;
    [SerializeField]
    private RoomEnterHandler _kitchenRoom;
    [SerializeField]
    private RoomEnterHandler _WCRoom;
    [SerializeField]
    private RoomEnterHandler _childRoom;
    [SerializeField]
    private RoomEnterHandler _atticRoom;

    private RoomEnterHandler _currentRoom;


    private void Start()
    {
        _currentRoom = _livingRoom;
    }
    public void MoveNextRoom()
    {
        //TODO: ��������� ������� audiosource, �������� audiosource � ��������� �������, ���������� ������������� �������� ��� IsMonsterHere
        _currentRoom.IsMonsterHere = false;
        switch (_currentRoom.gameObject.name)
        {
            case "LivingRoom":
                _currentRoom = _basementRoom;
                break;
            case "Basement":
                _currentRoom = _garageRoom;
                break;
            case "Garage":
                _currentRoom = _parentsRoom;
                break;
            case "ParentsRoom":
                _currentRoom = _kitchenRoom;
                break;
            case "Kitchen":
                _currentRoom = _WCRoom;
                break;
            case "WC":
                _currentRoom = _childRoom;
                break;
            case "ChildRoom":
                _currentRoom = _atticRoom;
                break;
            case "Attic":
                //EndOfGame;
                break;
        }
        Debug.Log($"������ �������, � �������:{_currentRoom.name}");
        _currentRoom.IsMonsterHere = true;
    }


}
