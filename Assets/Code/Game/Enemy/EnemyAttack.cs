using UnityEngine;

namespace Code.Game.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _damage = 2f;
        [SerializeField] private float _cooldown = 1f;

        private readonly Collider2D[] _hits = new Collider2D[3];

        private float _timeNextAttack;

        public void Attack()
        {
            if (_timeNextAttack > Time.time)
                return;

            _animator.Attack();
            _timeNextAttack = Time.time + _cooldown;

            if (Hit(out IHealth health))
                health.TakeDamage(_damage);
        }

        private bool Hit(out IHealth health)
        {
            var hit = Physics2D.OverlapCircleNonAlloc(transform.position + transform.forward, 1, _hits);

            for (int i = 0; i < hit; i++)
            {
                if (_hits[i].TryGetComponent(out health))
                    return true;
            }

            health = null;
            return false;
        }
    }
}