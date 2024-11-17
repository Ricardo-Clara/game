using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    
    [Header("Weapon Shooting")]
    public AudioSource shootingChannel;
    public AudioClip M4Shot;
    public AudioClip P1911Shot;
    public AudioClip shotgunShot;
    public AudioClip sniperShot;
    public AudioClip uziShot;
    public AudioClip rpgShot;


    [Header("Weapon Reloading")]
    public AudioSource reloadingChannel;
    public AudioClip reloadingSound1911;
    public AudioClip reloadingSoundM4;
    public AudioClip reloadingSoundShotgun;
    public AudioClip reloadingSoundSniper;
    public AudioClip reloadingSoundUzi;
    public AudioClip reloadingSoundRPG;
    public AudioSource emptyMagazineSound1911;


    [Header("Throwables")]
    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;
    public AudioClip rpgExplosionSound;

    [Header("Zombie")]
    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;
    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioClip zombieSpitterAttack;
    public AudioClip zombieBossAttack;

    [Header("Player")]
    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDeath;
    public AudioClip gameOverSound;

    [Header("Soundtrack")]
    public AudioSource soundtrackChannel;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon) {
        switch (weapon) {
            case WeaponModel.Pistol:
                shootingChannel.PlayOneShot(P1911Shot);
                break;
            case WeaponModel.Rifle:
                shootingChannel.PlayOneShot(M4Shot);
                break;
            case WeaponModel.Shotgun:
                shootingChannel.PlayOneShot(shotgunShot);
                break;
            case WeaponModel.Sniper:
                shootingChannel.PlayOneShot(sniperShot);
                break;
            case WeaponModel.SMG:
                shootingChannel.PlayOneShot(uziShot);
                break;
            case WeaponModel.RPG:
                shootingChannel.PlayOneShot(rpgShot);
                break;
        }
    }

    public void PlayReloadingSound(WeaponModel weapon) {
        switch (weapon) {
            case WeaponModel.Pistol:
                reloadingChannel.PlayOneShot(reloadingSound1911);
                break;
            case WeaponModel.Rifle:
                reloadingChannel.PlayOneShot(reloadingSoundM4);
                break;
            case WeaponModel.Shotgun:
                reloadingChannel.PlayOneShot(reloadingSoundShotgun);
                break;
            case WeaponModel.Sniper:
                reloadingChannel.PlayOneShot(reloadingSoundSniper);
                break;
            case WeaponModel.SMG:
                reloadingChannel.PlayOneShot(reloadingSoundUzi);
                break;
            case WeaponModel.RPG:
                reloadingChannel.PlayOneShot(reloadingSoundRPG);
                break;
        }
    }
}
