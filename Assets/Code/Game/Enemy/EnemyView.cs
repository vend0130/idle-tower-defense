using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [field: SerializeField] public Transform EnemyTransform { get; private set; }
        [SerializeField] private EnemyMove _move;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private float _minimalDistanceToHero = .75f;

        private Transform _target;
        private bool _isMoving;

        public void Init(Transform hero) =>
            _target = hero;

        private void Update()
        {
            if (_target == null)
            {
                _move.StopMove();
                return;
            }

            float distance = Vector2.Distance(EnemyTransform.position, _target.position);

            if (HeroNotReached(distance))
            {
                _move.StopMove();
                _attack.Attack();
            }
            else
            {
                _move.Move(EnemyTransform, distance, _target.position);
            }

            _move.Rotation(EnemyTransform, _target.position);
        }

        private bool HeroNotReached(float distance) =>
            distance <= _minimalDistanceToHero;
    }
}