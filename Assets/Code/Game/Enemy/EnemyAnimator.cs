using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _moveHash = Animator.StringToHash("Move");
        private readonly int _attackHash = Animator.StringToHash("Attack");

        public void StarMove() =>
            _animator.SetBool(_moveHash, true);

        public void StopMove() =>
            _animator.SetBool(_moveHash, false);

        public void Attack() =>
            _animator.SetTrigger(_attackHash);
    }
}