using Item;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private static readonly int DoDie = Animator.StringToHash("DoDie");
        private static readonly int DoRespawn = Animator.StringToHash("DoRespawn");

        [SerializeField] public int maxHealth = 3;
        [SerializeField] public int health = 1;
        [HideInInspector] public int unlockedHealth;
        [SerializeField] private AudioClip dieClip;

        [Header("Respawn")] [SerializeField] private float respawnInitTime = 4f;
        [SerializeField] private float respawnFinishTime = 64f;
        [SerializeField] private AudioClip respawnClip;

        private Animator _animator;
        private CharacterController _controller;
        private GameObject _drop;
        private GameObject _mesh;
        private GameObject _respawnParticles;
        private Vector3 _respawnPoint;
        private AudioSource _sound;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _controller = GetComponent<CharacterController>();
            _sound = GetComponent<AudioSource>();
            _respawnPoint = transform.position;
            _respawnParticles = transform.Find("RespawnParticles").gameObject;
            _mesh = transform.Find("Mesh").gameObject;
            _drop = transform.Find("Drop").gameObject;
            unlockedHealth = health;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsDead()) return;
            switch (other.gameObject.tag)
            {
                case "Hammer":
                    health -= other.gameObject.GetComponent<Hammer>().damage;
                    if (health <= 0)
                    {
                        health = 0;
                        _animator.SetTrigger(DoDie);
                        _sound.clip = dieClip;
                        _sound.Play();
                        foreach (var component in GetComponents<MonoBehaviour>())
                        {
                            component.enabled = false;
                        }

                        enabled = true;
                        if (_respawnParticles) _respawnParticles.SetActive(true);
                        Invoke(nameof(Respawn), respawnInitTime);
                    }

                    break;
            }
        }


        private void Respawn()
        {
            _mesh.SetActive(false);
            _sound.clip = respawnClip;
            _controller.enabled = false;
            var drop = Instantiate(_drop);
            drop.transform.position = transform.position;
            drop.SetActive(true);
            transform.position = _respawnPoint;
            Physics.SyncTransforms();
            _respawnParticles.SetActive(false);
            if (respawnFinishTime >= 0) Invoke(nameof(Resurrect), respawnFinishTime);
        }

        private void Resurrect()
        {
            _animator.SetTrigger(DoRespawn);
            _sound.Play();
            _controller.enabled = true;
            _mesh.SetActive(true);
            health = 3;
            foreach (var component in GetComponents<MonoBehaviour>())
            {
                component.enabled = true;
            }
        }

        public bool IsDead()
        {
            return health <= 0;
        }
    }
}