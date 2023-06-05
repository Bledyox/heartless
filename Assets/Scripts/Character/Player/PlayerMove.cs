using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        [Header("Walking")] [SerializeField] public float walkSpeed = 8f;
        [SerializeField] private InputActionReference walkInput;

        [Header("Running")] [SerializeField] public float runSpeed = 16f;
        [SerializeField] private InputActionReference runInput;

        [Header("Turning")] [SerializeField] private float turnSpeed = 8f;

        private Animator _animator;
        private Transform _camera;
        private CharacterController _controller;

        private float _moveSpeed;

        private void Awake()
        {
            runInput.action.canceled += ctx => _moveSpeed = walkSpeed;
            runInput.action.started += ctx => _moveSpeed = runSpeed;
        }

        private void Start()
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            _controller = gameObject.GetComponent<CharacterController>();
            _animator = gameObject.GetComponentInChildren<Animator>();
            _moveSpeed = walkSpeed;
        }

        private void Update()
        {
            var direction = walkInput.action.ReadValue<Vector2>();

            var move = new Vector3(direction.x, 0f, direction.y);
            move = _camera.forward * move.z + _camera.right * move.x;
            move.y = 0f;
            _controller.Move(move * (Time.deltaTime * _moveSpeed));
            _animator.SetBool(IsWalk, !move.Equals(Vector3.zero) && _moveSpeed.Equals(walkSpeed));
            _animator.SetBool(IsRun, !move.Equals(Vector3.zero) && _moveSpeed.Equals(runSpeed));

            var isTurn = direction != Vector2.zero;
            if (!isTurn) return;
            var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            var rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }

        private void OnEnable()
        {
            runInput.action.Enable();
            walkInput.action.Enable();
        }

        private void OnDisable()
        {
            runInput.action.Disable();
            walkInput.action.Disable();
        }
    }
}