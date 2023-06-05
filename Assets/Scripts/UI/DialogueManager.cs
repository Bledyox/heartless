using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoBehaviour
    {
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        [SerializeField] private AudioClip navigateClip;
        
        private Animator _animator;
        private AudioSource _source;

        private Queue<Dialogue> _dialogues;
        private Text _dialogueText;
        private Text _nameText;
        private Queue<string> _sentences;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
            _source.clip = navigateClip;
            var dialogueBox = transform.Find("DialogueBox").transform;
            _dialogueText = dialogueBox.Find("Dialogue").GetComponent<Text>();
            _nameText = dialogueBox.Find("Name").GetComponent<Text>();
            _dialogues = new Queue<Dialogue>();
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue[] dialogues)
        {
            _animator.SetBool(IsOpen, true);
            _dialogues.Clear();
            foreach (var element in dialogues)
            {
                _dialogues.Enqueue(element);
            }

            NextDialogue();
        }

        private void NextDialogue()
        {
            if (_dialogues.Count == 0)
            {
                EndDialogue();
                return;
            }

            var dialogue = _dialogues.Dequeue();
            _nameText.text = dialogue.name;
            
            _sentences.Clear();
            foreach (var element in dialogue.sentences)
            {
                _sentences.Enqueue(element);
            }
            NextSentence();
        }

        public void NextSentence()
        {
            _source.Play();
            if (_sentences.Count == 0)
            {
                NextDialogue();
                return;
            }

            var sentence = _sentences.Dequeue();
            _dialogueText.text = sentence;
        }

        public void EndDialogue()
        {
            _animator.SetBool(IsOpen, false);
        }
    }
}