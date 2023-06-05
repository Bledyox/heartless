using UnityEngine;

namespace Utils
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] private float animationSpeed = 25f;

        private void Update()
        {
            transform.Rotate(Vector3.up * (animationSpeed * Time.deltaTime));
        }
    }
}