using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        
        private void Start()
        {
            Cursor.visible = false;
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("Game");
        }
        
    }
}