using Audio;
using DG.Tweening;
using Player;
using Rooms;
using UnityEditor;
using UnityEngine;

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
    
    [SerializeField]
    private AudioClip endGameClip;
    [SerializeField]
    private float transitionDuration = 3.0f;
    [SerializeField]
    private CanvasGroup transitionScreen;

    private RoomEnterHandler _currentRoom;


    private void Start()
    {
        _currentRoom = _livingRoom;
        _currentRoom.IsMonsterHere = true;
    }
    
    public void MoveNextRoom()
    {
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
                Debug.Log("EndGame");
                EndGame();
                break;
        }
        Debug.Log($"������ �������, � �������:{_currentRoom.name}");
        _currentRoom.IsMonsterHere = true;
    }

    private void EndGame()
    {
        InputLimiter.Instance.LimitInput(true);
        AudioManager.Instance.StopBGM();
            
        AudioManager.Instance.PlaySoundEffect(endGameClip);

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(7.0f);
        sequence.AppendCallback(() =>
        {
            AudioManager.Instance.PlaySoundEffect(endGameClip);
            AudioManager.Instance.FadeOutAll(transitionDuration);
        });
        sequence.AppendInterval(1.0f);
        sequence.Append(transitionScreen.DOFade(3.0f, transitionDuration));
        sequence.AppendCallback(() =>
        {
#if (UNITY_EDITOR)
            EditorApplication.ExitPlaymode();
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            Screen.fullScreen = false;
            Application.ExternalEval("window.open('" + "about:blank" + "','_self')");
#endif
        });
    } 
}
