using UnityEngine;

public class NormalZombie : Zombie
{
    [field: SerializeField] public override int MovementSpeed { get; protected set; } = 1;
}
