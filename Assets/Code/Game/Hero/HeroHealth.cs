using UnityEngine;

namespace Code.Game.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        public void TakeDamage(float damage)
        {
            Debug.Log(damage);
        }
    }
}