using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private string robotType;   //  Type of a robot defined in Constants

    [SerializeField]
    GameObject missileprefab;   //  Missile prefab
    
    //  Sounds
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

    public int health;
    public int range;
    public float fireRate;
    public Transform missileFireSpot;
    UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
    private float timeLastFired;
    private bool isDead;
    public Animator robot;


    void Start()
    {
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) //  Dead Men Tell No Tales
        {
            return;
        }
        transform.LookAt(player);   //  look at player
        agent.SetDestination(player.position);  //  go to player

        if (Vector3.Distance(transform.position, player.position) < range   
        && Time.time - timeLastFired > fireRate)    //  if player is in range and gun is ready
        {
            timeLastFired = Time.time;  
            fire();                             //  shoot the player
        }
    }

    private void fire()
    {
        GameObject missile = Instantiate(missileprefab);   //   create missile 
        missile.transform.position = missileFireSpot.transform.position;    //  at position and 
        missile.transform.rotation = missileFireSpot.transform.rotation;    //  at rotation of missileFireSpot

        GetComponent<AudioSource>().PlayOneShot(fireSound); //  play sound
        robot.Play("Fire"); //  play animation
    }

    public void TakeDamage(int amount)
    {
        if (isDead) //  don't touch him, he's already dead
        {
            return;
        }
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
            Game.RemoveEnemy();
            GetComponent<AudioSource>().PlayOneShot(deathSound);    //  play sound
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);  //  play sound
        }
    }

    IEnumerator DestroyRobot()  //  robot destruction coroutine
    {
        yield return new WaitForSeconds(1.5f);  //  some time before destroying game object
        Destroy(gameObject);
    }
}
