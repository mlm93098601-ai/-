using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Cat : MonoBehaviour
    {
        [Header("猫出现的时长")]
        public float showTime;
        private Animator _animator;
        public static event EventHandler OnCatHit ;
        private bool _isHit;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _isHit = false;
            Invoke(nameof(Hide), showTime);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (!_isHit)
            {
                OnCatHit?.Invoke(this,EventArgs.Empty);
                _isHit = true;
            }
            _animator.SetTrigger("hit");
        }
        
    }
}