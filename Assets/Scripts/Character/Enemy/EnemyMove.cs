using UnityEngine;

namespace Character.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyMove : MonoBehaviour
    {
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float moveDistance = 16f;
        [SerializeField] private float standDistance = 2f;

        private Animator _animator;
        private CharacterController _controller;
        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _animator = GetComponentInChildren<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _player.position);
            if (distance <= standDistance)
            {
                transform.LookAt(_player);
                _animator.SetBool(IsWalk, false);
                return;
            }

            if (distance <= moveDistance)
            {
                transform.LookAt(_player);
                _controller.Move(transform.forward * (moveSpeed * Time.deltaTime));
                _animator.SetBool(IsWalk, true);
            }
            else
            {
                _animator.SetBool(IsWalk, false);
            }
        }
    }
}