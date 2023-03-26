using TMPro;
using UnityEngine;

namespace UI
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTMP;
        private int _currentScore;
        
        public void UpdateScore(int points)
        {
            _currentScore += points;
            _scoreTMP.text = _currentScore.ToString();
        }
    }
}