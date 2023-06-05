using System;
using System.Collections.Generic;
using Character.Enemy;
using UnityEngine;

namespace Character.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerHealth : MonoBehaviour
    {
        private static readonly int DoDie = Animator.StringToHash("DoDie");
        private static readonly int DoRespawn = Animator.StringToHash("DoRespawn");

        [Header("Health")] [SerializeField] public int maxHealth = 10;
        [SerializeField] public int health = 5;
        [SerializeField] private int heartScore = 1;
        [SerializeField] private int greatHeartScore = 8;
        [SerializeField] private List<int> heartBreakPoints;

        [Header("Die")] [SerializeField] private float respawnInitTime = 2f;
        [SerializeField] private float respawnFinishTime = 2f;
        [SerializeField] private AudioClip dieClip;
        [SerializeField] private AudioClip respawnClip;

        private int _unlockedHealth;
        [HideInInspector] public int hearts;
        private int _breakPoint = 0;

        private Animator _animator;
        private GameObject _mesh;
        private GameObject _respawnParticles;
        private Vector3 _respawnPoint;
        private AudioSource _sound;


        private void Start()
        {
            _unlockedHealth = health;
            _animator = GetComponentInChildren<Animator>();
            _sound = GetComponent<AudioSource>();
            _respawnParticles = transform.Find("RespawnParticles").gameObject;
            _mesh = transform.Find("Mesh").gameObject;
            _respawnPoint = transform.position;
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        private void Respawn()
        {
            _mesh.SetActive(false);
            transform.position = _respawnPoint;
            Physics.SyncTransforms();
            Invoke(nameof(Resurrect), respawnFinishTime);
        }

        private void Resurrect()
        {
            _animator.SetTrigger(DoRespawn);
            _sound.clip = respawnClip;
            _sound.Play();
            _mesh.SetActive(true);
            _respawnParticles.SetActive(false);
            health = _unlockedHealth;
            foreach (var component in GetComponents<MonoBehaviour>())
            {
                component.enabled = true;
            }
        }

        private void HeartsCalculation(int value)
        {
            hearts += value;
            if (_breakPoint == -1 || heartBreakPoints.Count < 0) return;
            if (heartBreakPoints[_breakPoint] > hearts) return;
            hearts -= heartBreakPoints[_breakPoint];
            if (_breakPoint < heartBreakPoints.Count) _breakPoint++;
            else _breakPoint = -1;
            _unlockedHealth++;
            health = _unlockedHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsDead()) return;
            switch (other.gameObject.tag)
            {
                case "Enemy":
                    health -= other.gameObject.GetComponent<EnemyAttack>().damage;
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
                        hearts = 0;
                        if (_respawnParticles) _respawnParticles.SetActive(true);
                        Invoke(nameof(Respawn), respawnInitTime);
                    }

                    break;
                case "GreatHeart":
                    HeartsCalculation(greatHeartScore);
                    health = _unlockedHealth;
                    break;
                case "Heart":
                    HeartsCalculation(heartScore);
                    if (health >= _unlockedHealth) return;
                    health++;
                    break;
            }
        }
    }
}