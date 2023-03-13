using Code.Extensions;
using UnityEngine;

namespace Code.Game.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        public void Rotation(Transform body, Transform target) =>
            body.LookAt2D(target.position);
    }
}