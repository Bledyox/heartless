using UI;
using UnityEngine;

namespace Event
{
    [RequireComponent(typeof(SphereCollider))]
    public class DialogueEvent : MonoBehaviour
    {
        [SerializeField] private Dialogue[] dialogues;
        [SerializeField] private bool destroyAfterTrigger;

        private DialogueManager _dialogueManager;

        private void Start()
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            _dialogueManager.StartDialogue(dialogues);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            _dialogueManager.EndDialogue();
            if (destroyAfterTrigger) Destroy(gameObject);
        }
    }
}