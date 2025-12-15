using TMPro;
using UniRx;
using UnityEngine;

    public class UIViewer : MonoBehaviour
    {
        [SerializeField, Header("‘Ì—Í")]
        private TextMeshProUGUI _health;


        public void SetHealth(int health)
        {
            _health.text = health.ToString();
        }
    }