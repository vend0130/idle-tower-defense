using Code.Game.Hero;
using UnityEngine;
using Zenject;

namespace Code.Game.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EnemyMove _move;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private Transform _current;
        [SerializeField] private float _minimalDistanceToHero = .75f;

        private Transform _target;
        private bool _isMoving;

        [Inject]
        public void Constructor(HeroRotation hero)
        {
            _target = hero.transform;
        }

        private void Update()
        {
            if (_target == null)
                return;

            float distance = Vector2.Distance(_current.position, _target.position);

            if (HeroNotReached(distance))
            {
                _move.StopMove();
                _attack.Attack();
            }
            else
            {
                _move.Move(_current, distance, _target.position);
            }

            _move.Rotation(_current, _target.position);
        }

        private bool HeroNotReached(float distance) =>
            distance <= _minimalDistanceToHero;
    }
}