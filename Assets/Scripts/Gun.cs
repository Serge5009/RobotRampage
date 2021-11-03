using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate;
    protected float lastFireTime;
    public Ammo ammo;
    public AudioClip liveFire;
    public AudioClip dryFire;

    public float zoomFactor;
    public int range;
    public int damage;

    private float zoomFOV;
    private float zoomSpeed = 6;

    void Start()
    {
        zoomFOV = Constants.CameraDefaultZoom / zoomFactor;
        lastFireTime = Time.time - 10;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(1))        //  if RMB pressed
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,   //  zoom
           zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else                                //  if RMB not pressed
        {
            Camera.main.fieldOfView = Constants.CameraDefaultZoom;          //  normal FOV
        }
    }

            protected void Fire()
    {
        if (ammo.HasAmmo(tag))
        {
            GetComponent<AudioSource>().PlayOneShot(liveFire);  //  sound
            ammo.ConsumeAmmo(tag);                      //  ammo--
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(dryFire);   //  sound
        }
        GetComponentInChildren<Animator>().Play("Fire");    //  animation

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));   //  cast the ray from gun
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            processHit(hit.collider.gameObject);
        }
    }

    private void processHit(GameObject hitObject)
    {
        if (hitObject.GetComponent<Player>() != null)   //  for player
        {
            hitObject.GetComponent<Player>().TakeDamage(damage);    //  damage
        }
        if (hitObject.GetComponent<Robot>() != null)    //  for robot
        {   
            hitObject.GetComponent<Robot>().TakeDamage(damage);     //  damage
        }
    }
}
