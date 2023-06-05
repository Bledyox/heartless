using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerJump : MonoBehaviour
    {
        private static readonly int IsJump = Animator.StringToHash("IsJump");
        private static readonly int DoJump = Animator.StringToHash("DoJump");

        [Header("Gravity")] [SerializeField] private float gravityValue = -9.81f;

        [Header("Jumping")] [SerializeField] private float jumpHeight = 1f;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private AudioClip jumpClip;

        private Animator _animator;
        private CharacterController _controller;
        private bool _isGrounded;
        private AudioSource _sound;
        private Vector3 _velocity;

        private void Start()
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
            _controller = gameObject.GetComponent<CharacterController>();
            _sound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            var isJump = _isGrounded && jumpInput.action.triggered;
            if (isJump)
            {
                _velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                _animator.SetTrigger(DoJump);
                _animator.SetBool(IsJump, true);
                _sound.clip = jumpClip;
                _sound.Play();
            }

            _velocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            var isLand = _isGrounded && _velocity.y < 0f;
            if (isLand)
            {
                _velocity.y = 0f;
                _animator.SetBool(IsJump, false);
            }

            _isGrounded = _controller.isGrounded;
        }

        private void OnEnable()
        {
            jumpInput.action.Enable();
        }

        private void OnDisable()
        {
            jumpInput.action.Disable();
        }
    }
}