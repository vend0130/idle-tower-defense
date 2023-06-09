﻿using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _damage = 2f;
        [SerializeField] private float _cooldown = 1f;


        private readonly Collider2D[] _hits = new Collider2D[6];

        private const float CastRadius = .1f;
        private const float DistanceAttack = .75f;

        private float _timeNextAttack;
        private UnitType _target = UnitType.Hero;

        public void Attack()
        {
            if (_timeNextAttack > Time.time)
                return;

            _animator.Attack();
            _timeNextAttack = Time.time + _cooldown;

            if (Hit(out IHealth health))
                health.TakeDamage(_damage);
        }

        public void ChangeTarget(UnitType target) =>
            _target = target;

        private bool Hit(out IHealth health)
        {
            var hit = Physics2D
                .OverlapCircleNonAlloc(transform.position + transform.up * DistanceAttack, CastRadius, _hits);

            for (int i = 0; i < hit; i++)
            {
                if (_hits[i].TryGetComponent(out health) && health.Current != transform && health.Unit == _target)
                    return true;
            }

            health = null;
            return false;
        }
    }
}