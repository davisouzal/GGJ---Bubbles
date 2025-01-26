using UnityEngine;

public interface IPlayer : IActivatable, IEntity
{
    void playerMovement(){}
    void Die();
}
