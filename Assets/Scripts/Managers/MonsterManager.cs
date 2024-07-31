using Audio;
using DG.Tweening;
using Player;
using Rooms;
using UnityEditor;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private Room _livingRoom;
    [SerializeField]
    private Room _basementRoom;
    [SerializeField]
    private Room _garageRoom;
    [SerializeField]
    private Room _parentsRoom;
    [SerializeField]
    private Room _kitchenRoom;
    [SerializeField]
    private Room _WCRoom;
    [SerializeField]
    private Room _childRoom;
    [SerializeField]
    private Room _atticRoom;
    
    [SerializeField]
    private AudioClip endGameClip;
    [SerializeField]
    private float transitionDuration = 3.0f;
    [SerializeField]
    private CanvasGroup transitionScreen;

    private Room _currentRoom;


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
