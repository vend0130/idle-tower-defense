using System.Collections.Generic;
using Code.Factories.Enemies;
using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [field: SerializeField] public Transform EnemyTransform { get; private set; }
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public ControlledView ControlledView { get; private set; }
        [field: SerializeField] public EnemyHealth Health { get; private set; }
        [SerializeField] private EnemyMove _move;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private ControlledView _controlled;
        [SerializeField] private float _minimalDistanceToHero = .75f;
        [SerializeField] private List<Transform> _targets = new List<Transform>();

        private Transform _target;
        private bool _isMoving;
        private IEnemiesPool _pool;

        private void Update()
        {
            ChangeTarget();

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

        public void Spawn(Transform hero, Vector2 spawnPoint, IEnemiesPool pool)
        {
            _pool = pool;
            _health.Reset();
            _controlled.ChangeControlledState(false);
            _targets.Clear();

            AddTarget(hero);

            gameObject.SetActive(false);
            ControlledView.ChangeControlledState(false);
            transform.position = spawnPoint;
            gameObject.SetActive(true);
        }

        public void AddTarget(Transform target)
        {
            if (!_targets.Contains(target) && target != EnemyTransform)
                _targets.Add(target);
        }

        public void RemoveTarget(Transform target) =>
            _targets.Remove(target);

        private void ChangeTarget()
        {
            if (ControlledView.IsControlled)
            {
                _pool.TryChangeTarget(transform.position, out _target);
                return;
            }

            if (_targets.Count == 0 || _targets[0] == null)
            {
                _target = null;
                return;
            }

            float distance = Vector2.Distance(EnemyTransform.position, _targets[0].position);
            _target = _targets[0];

            for (int i = 1; i < _targets.Count; i++)
            {
                float newDistance = Vector2.Distance(EnemyTransform.position, _targets[i].position);

                if (newDistance < distance)
                {
                    distance = newDistance;
                    _target = _targets[i];
                }
            }
        }

        private bool HeroNotReached(float distance) =>
            distance <= _minimalDistanceToHero;
    }
}