using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference pauseInput;
        [SerializeField] private AudioClip navigateClip;

        private GameObject _controlsState;
        private GameObject _gameState;
        private GameObject _pauseState;
        private AudioSource _source;

        private void Start()
        {
            _gameState = transform.Find("GameState").gameObject;
            _gameState.SetActive(true);
            _pauseState = transform.Find("PauseState").gameObject;
            _pauseState.SetActive(false);
            _controlsState = transform.Find("ControlsState").gameObject;
            _controlsState.SetActive(false);
            _source = GetComponent<AudioSource>();
            _source.clip = navigateClip;
        }

        private void Update()
        {
            if (pauseInput.action.triggered)
            {
                PauseState();
            }
        }

        private void OnEnable()
        {
            pauseInput.action.Enable();
        }

        private void OnDisable()
        {
            pauseInput.action.Disable();
        }

        public void GameState()
        {
            _source.Play();
            Time.timeScale = 1f;
            _gameState.SetActive(true);
            _pauseState.SetActive(false);
            _controlsState.SetActive(false);
        }

        public void PauseState()
        {
            _source.Play();
            Time.timeScale = 0f;
            _gameState.SetActive(false);
            _pauseState.SetActive(true);
            _controlsState.SetActive(false);
        }

        public void ControlsState()
        {
            _source.Play();
            Time.timeScale = 0f;
            _gameState.SetActive(false);
            _pauseState.SetActive(false);
            _controlsState.SetActive(true);
        }

        public void ExitState()
        {
            _source.Play();
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}