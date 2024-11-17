using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 200;
    public AmmoType ammoType;

    public enum AmmoType { Pistol, Rifle, Shotgun, Sniper, Uzi, RPG }
}
