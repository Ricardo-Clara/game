using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int healthPoints = 100;
    public GameObject bloodScreen;
    private PlayerInput playerInput;
    private new Collider collider;
    public TextMeshProUGUI healthText;
    public GameObject[] gameOverUI;
    private CharacterController characterController;
    public ScoreAndLevel scoreAndLevel;

    public bool isDead;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        characterController.detectCollisions = false;
        collider = GetComponent<Collider>();
    }

    private void Start() {
        healthText.text = $"Health: {healthPoints}";
        isDead = false;

        scoreAndLevel.Reset();
    }


    public void TakeDamage(int damage) {
        healthPoints -= damage;

        if (healthPoints <= 0) {
            PlayerDead();
            isDead = true;
        } else {
            StartCoroutine(ShowBloodScreen());
            healthText.text = $"Health: {healthPoints}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        playerInput.enabled = false;
        collider.enabled = false;

        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverSound;
        SoundManager.Instance.playerChannel.PlayDelayed(2f);
        SoundManager.Instance.soundtrackChannel.enabled = false;
        SoundManager.Instance.zombieChannel.enabled = false;
        SoundManager.Instance.zombieChannel2.enabled = false;
        SoundManager.Instance.throwablesChannel.enabled = false;
        SoundManager.Instance.reloadingChannel.enabled = false;
        SoundManager.Instance.shootingChannel.enabled = false;
        SoundManager.Instance.emptyMagazineSound1911.enabled = false;

        GetComponentInChildren<Animator>().enabled = true;
        healthText.enabled = false;

        GetComponent<ScreenDeath>().StartFade();

        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(ShowGameOver());

        scoreAndLevel.UpdateScore(ZombieManager.Instance.currentWave);
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1f);
        foreach (var ui in gameOverUI)
        {
            ui.SetActive(true);
        }
    }

    private IEnumerator ShowBloodScreen()
    {
        if (bloodScreen.activeInHierarchy == false) {
            bloodScreen.SetActive(true);
        }

        var image = bloodScreen.GetComponentInChildren<UnityEngine.UI.Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;
 
        float duration = 1.5f;
        float elapsedTime = 0f;
 
        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
 
            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
 
            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;
 
            yield return null; ; // Wait for the next frame.
        }

        if (bloodScreen.activeInHierarchy) {
            bloodScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("ZombieHand")) {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
    }
}
