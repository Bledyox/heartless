using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event
{
    public class GameEndEvent : MonoBehaviour
    {
        [SerializeField] private float time = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            Invoke(nameof(End), time);
        }

        private void End()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}