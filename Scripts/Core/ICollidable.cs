
using UnityEngine;

namespace Assets.Scripts.Collidables
{
    public interface ICollidable
    {
        void CollisionWithPlayer(PlayerController playerController);
    }
}
