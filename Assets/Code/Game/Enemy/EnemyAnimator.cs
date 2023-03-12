using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _moveHas = Animator.StringToHash("Move");

        public void StarMove() =>
            _animator.SetBool(_moveHas, true);

        public void StopMove() =>
            _animator.SetBool(_moveHas, false);

        public void Attack()
        {
        }
    }
}