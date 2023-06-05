using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MainStateManager : MonoBehaviour
    {
        [SerializeField] private AudioClip navigateClip;

        private GameObject _mainState;
        private GameObject _creditsState;
        private AudioSource _source;

        private void Start()
        {
            _mainState = transform.Find("MainState").gameObject;
            _mainState.SetActive(true);
            _creditsState = transform.Find("CreditsState").gameObject;
            _creditsState.SetActive(false);
            _source = GetComponent<AudioSource>();
            _source.clip = navigateClip;
        }

        public void MainState()
        {
            _source.Play();
            _mainState.SetActive(true);
            _creditsState.SetActive(false);
        }

        public void PlayState()
        {
            _source.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void CreditsState()
        {
            _source.Play();
            _mainState.SetActive(false);
            _creditsState.SetActive(true);
        }

        public void QuitState()
        {
            _source.Play();
            Application.Quit();
        }
    }
}