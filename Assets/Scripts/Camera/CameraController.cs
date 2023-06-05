using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    [RequireComponent(typeof(Animator))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputActionReference swapInput;
        [SerializeField] private List<GameObject> firstPersonDisableParts;
    
        private Animator _animator;
        private bool _isFirstPerson;
    
        private void OnEnable()
        {
            swapInput.action.Enable();
        }

        private void OnDisable()
        {
            swapInput.action.Disable();
        }
    
    
        public void Start ()
        {
            _animator = GetComponent<Animator>();
            _isFirstPerson = true;
            swapInput.action.performed += _ => Swap();
        }
   
        private void Swap ()
        {
            _animator.Play(_isFirstPerson ? "FirstPerson" : "ThirdPerson");
            if (_isFirstPerson)
            {
                Invoke(nameof(SwapFinish), 1.2f);
            }
            else
            {
                SwapFinish();
            }
        }
        
        private void SwapFinish()
        {
            firstPersonDisableParts.ForEach(part => part.SetActive(!_isFirstPerson));
            _isFirstPerson = !_isFirstPerson;
        }
    }
}
