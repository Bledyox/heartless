using System;
using Item;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour
    {
        private static readonly int DoSwing = Animator.StringToHash("DoSwing");

        [Header("Weapon")] [SerializeField] private GameObject weapon;

        [Header("Attacking")] [SerializeField] private InputActionReference attackInput;

        private Animator _animator;
        private float _delay;
        private Hammer _hammer;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            weapon.SetActive(false);
        }

        public void Update()
        {
            if (!_hammer) return;
            _delay += Time.deltaTime;
            var isAttack = attackInput.action.triggered && _hammer.attackRate < _delay;
            if (!isAttack) return;
            _hammer.Use();
            _animator.SetTrigger(DoSwing);
            _delay = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Hammer"))
            {
                weapon.SetActive(true);
                _hammer = weapon.GetComponent<Hammer>();
            }
        }

        private void OnEnable()
        {
            attackInput.action.Enable();
        }

        private void OnDisable()
        {
            attackInput.action.Enable();
        }
    }
}