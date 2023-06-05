using UnityEngine;

namespace Event
{
    public class ActivateEvent : MonoBehaviour
    {
        [SerializeField] private GameObject activateObject;
        [SerializeField] private bool activate = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            if (activateObject) activateObject.SetActive(activate);
        }
    }
}