using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int armor;
    public GameUI gameUI;
    private GunEquipper gunEquipper;
    private Ammo ammo;

    void Start()
    {
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
    }

    public void TakeDamage(int amount) //G
    {
         int healthDamage = amount;
         if (armor > 0)
         {
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;

            // If there is still armor, don't need to process
            // health damage
            if (effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                return;
            }
            armor = 0;
        }

        health -= healthDamage;
        Debug.Log("Health is " + health);
        if (health <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    ///     PICKUPS     ///

    public void PickUpItem(int pickupType)
    {   //  using constants to determine the type of pickup
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                pickupArmor();
                break;
            case Constants.PickUpHealth:
                pickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                pickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                pickupPisolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                pickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Bad pickup type passed" + pickupType);
                break;
        }
    }

    private void pickupHealth()                                 //  health
    {
        health += 50;
        if (health > 200)
        {   //  limit to 200
            health = 200;
        }
    }
    private void pickupArmor()                                  //  armor
    {
        armor += 15;
    }

    private void pickupAssaultRifleAmmo()                       //  ammo rifle
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);
    }
    private void pickupPisolAmmo()                              //  ammo pistol
    {
        ammo.AddAmmo(Constants.Pistol, 20);
    }
    private void pickupShotgunAmmo()                            //  ammo shotgun
    {
        ammo.AddAmmo(Constants.Shotgun, 10);
    }
}
