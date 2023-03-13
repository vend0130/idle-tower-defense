using Code.Extensions;
using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _speed = 1f;

        public void StopMove() =>
            _animator.StopMove();

        public void Move(Transform current, float distance, Vector2 targetPoint)
        {
            float moveTime = _speed / distance * Time.deltaTime;
            current.position = Vector2.Lerp(current.position, targetPoint, moveTime);

            _animator.StarMove();
        }

        public void Rotation(Transform current, Vector2 targetPoint) =>
            current.LookAt2D(targetPoint);
    }
}