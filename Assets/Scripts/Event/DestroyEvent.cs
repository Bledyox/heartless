using UnityEngine;

namespace Event
{
    public class DestroyEvent : MonoBehaviour
    {
        [SerializeField] private GameObject[] destroyObjects;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            if (destroyObjects.Length <= 0) return;
            foreach (var destroyObject in destroyObjects)
            {
                Destroy(destroyObject);
            }
        }
    }
}