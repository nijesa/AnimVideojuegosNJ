using UnityEngine;

namespace AV2.Scripts
{
    public class CharacterAnimator
    {
        private readonly Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Speed");

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void UpdateSpeed(float speed)
        {
            Debug.Log($"Trying to put the input:{speed}");
            _animator.SetFloat(_speedHash, speed);
        }
        
    }
}