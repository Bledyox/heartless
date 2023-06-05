using UnityEngine;

namespace Item
{
    public class GreatHeart : MonoBehaviour
    {
        public enum Type
        {
            Shadow,
            Light,
            Fire,
            Water,
            Wind,
            Earth,
        }

        [SerializeField] public Type type;
    }
}