using UnityEngine;

namespace Event
{
    [RequireComponent(typeof(SphereCollider))]
    public class CollectEvent : MonoBehaviour
    {
        [SerializeField] private bool destroyAfterTrigger;

        private GameObject _collectable;

        private void Start()
        {
            _collectable = transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            if (destroyAfterTrigger) Destroy(_collectable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            if (destroyAfterTrigger) Destroy(gameObject);
        }
    }
}