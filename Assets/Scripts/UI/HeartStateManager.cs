using System;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeartStateManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] heartStates;
        [SerializeField] private HeartColorStates[] heartColorStates;

        private Image _backgroundImage;
        private Image _foregroundImage;
        private Text _heartcount;

        private int _healthBuffer;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            if (heartStates.Length <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            _foregroundImage = transform.Find("Foreground").GetComponent<Image>();
            _backgroundImage = transform.Find("Background").GetComponent<Image>();
            _heartcount = transform.Find("Count").GetComponent<Text>();
            _playerHealth = FindObjectOfType<PlayerHealth>();
            SwitchState(_playerHealth.health);
            _healthBuffer = _playerHealth.health;
        }

        private void Update()
        {
            _heartcount.text = _playerHealth.hearts.ToString();
            if (_healthBuffer == _playerHealth.health) return;
            SwitchState(_playerHealth.health);
            _healthBuffer = _playerHealth.health;
        }

        private void SwitchState(int health)
        {
            if (health <= 0)
            {
                _foregroundImage.color = Color.clear;
                return;
            }

            var spriteIndex = (health - 1) % heartStates.Length;
            _foregroundImage.sprite = heartStates[spriteIndex];
            if (heartColorStates.Length <= 0) return;
            var colorIndex = health / (heartStates.Length + 1);
            _foregroundImage.color = heartColorStates[colorIndex].foreground;
            _backgroundImage.color = heartColorStates[colorIndex].background;
        }

        [Serializable]
        private class HeartColorStates
        {
            public Color foreground;
            public Color background;
        }
    }
}