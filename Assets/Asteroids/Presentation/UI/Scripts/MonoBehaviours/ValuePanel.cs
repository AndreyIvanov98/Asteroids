using TMPro;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.MonoBehaviours
{
    public class ValuePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _value;

        public void SetValue(string value) => 
            _value.text = value;
    }
}