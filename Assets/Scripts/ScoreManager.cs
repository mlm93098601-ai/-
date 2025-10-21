using System;
using TMPro;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        private int _score = 0;

        private void Start()
        {
            Cat.OnCatHit+=UpdateScore; 
        }

        private void OnDestroy()
        {
            Cat.OnCatHit-=UpdateScore; 
        }

        private void UpdateScore(Object o, EventArgs e)
        {
            _score++;
            scoreText.text = _score.ToString();
        }
    }
}