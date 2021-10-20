using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    GameUI gameUI;
    [SerializeField]
    private int pistolAmmo = 20;
    [SerializeField]
    private int shotgunAmmo = 10;
    [SerializeField]
    private int assaultRifleAmmo = 50;
    public Dictionary<string, int> tagToAmmo;


    void Awake()
    {
        tagToAmmo = new Dictionary<string, int>
        {
            { Constants.Pistol , pistolAmmo },
            { Constants.Shotgun , shotgunAmmo },
            { Constants.AssaultRifle , assaultRifleAmmo },
        };
    }
    void Start()
    {
        
    }

    public void AddAmmo(string tag, int ammo)   //  Increases number of AMMO by NUMBER
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        tagToAmmo[tag] += ammo;
    }

    public bool HasAmmo(string tag)             //  Boolean ammo check
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        return tagToAmmo[tag] > 0;
    }

    public int GetAmmo(string tag)              //  Returnes amount of AMMO
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        return tagToAmmo[tag];
    }

    public void ConsumeAmmo(string tag)         //  Decreases number of AMMO by 1
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        tagToAmmo[tag]--;
    }
}
