using System.Globalization;
using Febucci.UI.Core;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        
        [SerializeField]
        private TypewriterCore typewriter;
        [SerializeField]
        private float timeBetweenLines;

        private int _currentLine = -1;
        private DialogueInfo _currentDialogueInfo;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        void OnEnable()
        {
            typewriter.onTextShowed.AddListener(DisplayNextLine);
        }
        
        void OnDisable()
        {
            typewriter.onTextShowed.RemoveListener(DisplayNextLine);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (typewriter != null) return;
            Debug.LogError("No typewriter is assigned");
            gameObject.SetActive(false);
        }

        public void Show(DialogueInfo info)
        {
            Restart();
            _currentDialogueInfo = info;
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            _currentLine++;
            if (_currentLine < _currentDialogueInfo.dialogueLines.Length)
                typewriter.ShowText(_currentDialogueInfo.dialogueLines[_currentLine] + 
                                     $"<waitfor={timeBetweenLines.ToString(CultureInfo.InvariantCulture)}>");
            else
                typewriter.StartDisappearingText();
        }

        private void Restart()
        {
            typewriter.SkipTypewriter();
            _currentLine = -1;
        }
    }
}
