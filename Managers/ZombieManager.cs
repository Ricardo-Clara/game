using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ZombieSpawnChance {
    public GameObject zombiePrefab;
    public float chanceToSpawn;
}

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; set; }

    public int zombiesPerWave = 5;
    private float zombieMultiplier = 1.2f;
    public GameObject[] spawnPoints;

    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public GameObject player;
    public List<ZombieSpawnChance> zombies;
    public List<Enemy> zombiesAlive;

    public TextMeshProUGUI waveOverText;
    public TextMeshProUGUI cooldownCounterText;
    public TextMeshProUGUI currentWaveText;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


    private void Start() {
        StartWave();
    }

    private void StartWave() {
        zombiesAlive.Clear();

        currentWave++;
        currentWaveText.text = "Wave: " + currentWave.ToString();

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave() {
        int spawnedZombies = 0;

        while (spawnedZombies < zombiesPerWave) {
            for (int i = 0; i < spawnPoints.Length; i++) {
                if (spawnedZombies >= zombiesPerWave) {
                    break;
                }

                Vector3 spawnOffset = new(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
                Vector3 spawnPosition = spawnPoints[i].transform.position + spawnOffset;
                Vector3 direction = player.transform.position + new Vector3(0, 2, 0) - spawnPosition;

                //RaycastHit hit;
                if (Physics.Raycast(spawnPosition, direction, out RaycastHit hit, Mathf.Infinity)) {
                    if (hit.collider.gameObject != player) {
                        float randomValue = UnityEngine.Random.Range(0f, 1f);
                        float cummulativeProbability = 0f;

                        foreach(ZombieSpawnChance zombie in zombies) {
                            cummulativeProbability += zombie.chanceToSpawn;

                            if (randomValue <= cummulativeProbability) {
                                GameObject zombieInstance = Instantiate(zombie.zombiePrefab, spawnPosition, Quaternion.identity);
                                zombiesAlive.Add(zombieInstance.GetComponent<Enemy>());
                                spawnedZombies++;
                                break;
                            }
                        }
                    }
                }   
            }
            yield return new WaitForSeconds(spawnDelay);
        }

    }

    private void Update() {
        List<Enemy> deadZombies = new();

        foreach (Enemy zombie in zombiesAlive) {
            if (zombie.isDead) {
                deadZombies.Add(zombie);
            }
        }

        foreach (Enemy zombie in deadZombies) {
            zombiesAlive.Remove(zombie);
        }

        deadZombies.Clear();

        if (zombiesAlive.Count == 0 && !inCooldown) {
            StartCoroutine(WaveCooldown());
        }

        if (inCooldown) {
            cooldownCounter -= Time.deltaTime;
        } else {
            cooldownCounter = waveCooldown;
        }

        cooldownCounterText.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverText.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverText.gameObject.SetActive(false);

        zombiesPerWave = (int)(zombiesPerWave * zombieMultiplier);
        StartWave();
    }
}
