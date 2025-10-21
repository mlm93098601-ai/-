using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimeManager : MonoBehaviour
    {
        public TextMeshProUGUI timeText;
        [Header("游戏总时间")]
        public float gameTime;
        private bool canCountDown=false;

        private void Update()
        {
            if (canCountDown)
            {
                gameTime -= Time.deltaTime;
            }

            timeText.text = gameTime.ToString("F1");
        }

        public void StartCountTime(bool canCount)
        {
            this.canCountDown = canCount;
        }
    }
}