using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asteroids.Presentation.UI.Scripts.MonoBehaviours
{
    public class RestartWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private Button _restartButton;

        public void Setup(string value, UnityAction restartAction)
        {
            _scoreValue.text = value;
            _restartButton.onClick.AddListener(restartAction);
        }
    }
}