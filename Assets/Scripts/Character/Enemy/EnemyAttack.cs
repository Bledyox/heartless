using System.Collections;
using Character.Player;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private static readonly int DoAttack = Animator.StringToHash("DoAttack");

        [SerializeField] public int damage = 1;
        [SerializeField] private float attackRate = 0.5f;
        [SerializeField] private float attackDistance = 2f;
        [SerializeField] private AudioClip attackClip;

        private SphereCollider _damageArea;
        private Animator _animator;
        private AudioSource _sound;
        private float _delay;
        private Transform _player;
        private PlayerHealth _playerHealth;
        private EnemyHealth _health;

        private void Start()
        {
            _damageArea = GetComponent<SphereCollider>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _animator = GetComponentInChildren<Animator>();
            _sound = GetComponent<AudioSource>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _health = GetComponent<EnemyHealth>();
        }

        public void Update()
        {
            _delay += Time.deltaTime;
            if (_playerHealth.IsDead()) return;
            var distance = Vector3.Distance(transform.position, _player.position);
            var canAttack = distance <= attackDistance && attackRate < _delay;
            if (!canAttack) return;
            StopCoroutine(nameof(Attack));
            StartCoroutine(nameof(Attack));
            _animator.SetTrigger(DoAttack);
            _delay = 0;
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(.2f);
            _sound.clip = attackClip;
            _sound.Play();
            yield return new WaitForSeconds(.4f);
            _damageArea.enabled = true;
            yield return new WaitForSeconds(1f);
            _damageArea.enabled = false;
            if (!_playerHealth.IsDead()) yield break;
            if (_health.unlockedHealth < _health.maxHealth)
            {
                _health.unlockedHealth++;
                _health.health = _health.unlockedHealth;
            }
        }
    }
}