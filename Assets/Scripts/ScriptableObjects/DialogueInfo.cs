using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DialogueInfo", menuName = "ScriptableObject/DialogueInfo", order = 1)]
    public class DialogueInfo : ScriptableObject
    {
        public string[] dialogueLines;
    }
}