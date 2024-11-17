using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healthPoints = 100;
    [SerializeField] private int healthIncrementPerWave = 25;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Collider col;
    Transform player;

    [Header("Zombie Characteristics (The Attacking Distance is the Stopping Distance Of The Agent)")]
    public float chaseSpeed = 6f;
    public bool isDead;

    [Header("Zombie Attack")]
    public GameObject muzzleEffect;
    public Transform firePoint;
    public float projectileSpeed = 30f;
    public Transform lineOfSight;


    [Header("Miscellaneous")]
    public DropTable dropTable;
    public float deathAnimationDuration = 3f;
    public int bodyLifeTime = 25;

    private float attackingDistance;
    private Zombie zombie;

    

    private void Start() {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zombie = GetComponent<Zombie>();

        navMeshAgent.speed = chaseSpeed;
        attackingDistance = navMeshAgent.stoppingDistance;

        healthPoints += healthIncrementPerWave * (ZombieManager.Instance.currentWave - 1);
    }

    private void Update() {
        if (!isDead) {
           if (SoundManager.Instance.zombieChannel.isPlaying == false) {
               SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieChase);
           }
           if (animator.GetBool("isAttacking") == true) {
               LookAtPlayer();
           }
   
   
           navMeshAgent.SetDestination(player.position);
   
           float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
           
           if (!CheckLineOfSight()) {
               animator.SetBool("isAttacking", false);
               navMeshAgent.stoppingDistance = 0;
   
           } else if (distanceFromPlayer < attackingDistance  && CheckLineOfSight()) {
               navMeshAgent.stoppingDistance = attackingDistance;
               navMeshAgent.SetDestination(transform.position);

               
               animator.SetBool("isAttacking", true);
           } else {
               animator.SetBool("isAttacking", false);
               navMeshAgent.stoppingDistance= 0;
           }
        }
    }

    private bool CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 rayDirection = player.position - transform.position;

        if (Physics.Raycast(lineOfSight.position, rayDirection, out hit, 100)) {
            Debug.DrawRay(lineOfSight.position, rayDirection, Color.green);
            if (hit.transform.CompareTag("Player")) {
                return true;
            }
        }

        return false;
    }

    private void CheckZombieTypeSound()
    {
        switch (zombie.zombieType) {
            case Zombie.ZombieType.Normal:
                SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieAttack);
                break;
            case Zombie.ZombieType.Spitter:
                SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieSpitterAttack);
                break;
            case Zombie.ZombieType.Boss:
                SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieBossAttack);
                break;
        }
    }

    private void SpitAttack()
    {
        GameObject projectile = Instantiate(GlobalReferences.Instance.spitProjectile, firePoint.position, transform.rotation);

        projectile.transform.rotation = Quaternion.LookRotation(firePoint.forward);
        // Shoot the projectile
        projectile.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);


        Destroy(projectile, 5);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - navMeshAgent.transform.position;
        navMeshAgent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = navMeshAgent.transform.eulerAngles.y;
        navMeshAgent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void TakeDamage(int damage) {
        healthPoints -= damage;

        if (healthPoints <= 0) {
            isDead = true;
            int randomValue = Random.Range(1, 3); // 1 or 2

            animator.SetTrigger($"DIE{randomValue}");

            Destroy(col);
            Destroy(navMeshAgent);
            DropLoot();

            Destroy(gameObject, bodyLifeTime);

            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieDeath);
        } else {
            
            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieHurt);
            
        }
    }


    private void DropLoot() {
        if (dropTable != null) {
            GameObject loot = dropTable.GetDrop();
            if (loot != null) {
                GameObject instance = Instantiate(loot, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                instance.layer = LayerMask.NameToLayer("Drops");
            }
        }
    }
}
