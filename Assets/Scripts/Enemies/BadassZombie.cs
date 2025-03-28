using UnityEngine;

public class BadassZombie : Zombie
{
    [field: SerializeField] public override int MovementSpeed { get; protected set; } = 3;
}
