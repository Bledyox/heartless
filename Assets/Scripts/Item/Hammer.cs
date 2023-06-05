using System.Collections;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(BoxCollider))]
    public class Hammer : MonoBehaviour
    {
        [SerializeField] public float attackRate = 0.5f;
        [SerializeField] private AudioClip attackClip;
        [SerializeField] public int damage = 1;

        private BoxCollider _damageArea;
        private AudioSource _sound;
        private TrailRenderer _trailEffect;

        private void Start()
        {
            _damageArea = GetComponent<BoxCollider>();
            _sound = GetComponent<AudioSource>();
            _trailEffect = GetComponentInChildren<TrailRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
        }

        public void Use()
        {
            StopCoroutine(nameof(Swing));
            StartCoroutine(nameof(Swing));
        }

        private IEnumerator Swing()
        {
            yield return new WaitForSeconds(0.1f);
            _sound.clip = attackClip;
            _sound.Play();
            _damageArea.enabled = true;
            _trailEffect.enabled = true;
            yield return new WaitForSeconds(0.3f);
            _damageArea.enabled = false;
            yield return new WaitForSeconds(0.3f);
            _trailEffect.enabled = false;
            yield break;
        }
    }
}