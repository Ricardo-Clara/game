using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;
    public EnemyProjectile enemyProjectile;
    public int zombieDamage;
    public enum ZombieType { Normal, Spitter, Boss }
    public ZombieType zombieType;

    private void Start() {

        if (zombieType == ZombieType.Boss || zombieType == ZombieType.Normal)
        {
            zombieHand.damage = zombieDamage;
        } else if (zombieType == ZombieType.Spitter)
        {
            enemyProjectile.projectileDamage = zombieDamage;
        }

    }
}
