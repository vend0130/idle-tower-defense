using Code.Extensions;
using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _speed = 1f;

        private bool _isMoving;

        public void StopMove()
        {
            if (_isMoving)
                _animator.StopMove();

            _isMoving = false;
        }

        public void Move(Transform current, float distance, Vector2 targetPoint)
        {
            float moveTime = _speed / distance * Time.deltaTime;
            current.position = Vector2.Lerp(current.position, targetPoint, moveTime);

            if (!_isMoving)
                _animator.StarMove();

            _isMoving = true;
        }

        public void Rotation(Transform current, Vector2 targetPoint) =>
            current.LookAt2D(targetPoint);
    }
}