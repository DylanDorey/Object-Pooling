using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
//using the unity pool namespace

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [04/16/2024]
 * [A drone that spawns and attacks the player then returns to a drone pool]
 */

public class Drone : MonoBehaviour
{
    //pool of drones of type IObjectPool
    public IObjectPool<Drone> Pool { get; set; }

    //current health and max health of the drone
    public float currentHealth;

    [SerializeField]
    private float maxHealth = 100f;

    //the time before the drones remove themselves from the scene/self destruct
    [SerializeField]
    private float timeToSelfDestruct = 2f;

    private void Start()
    {
        //set the drones current health to their max health
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        //attack the player and self destruct on spawn
        AttackPlayer();
        StartCoroutine(SelfDestruct());
    }

    private void OnDisable()
    {
        //rest the drone when despawning
        ResetDrone();
    }

    /// <summary>
    /// Destroys the drone after a specified amount of time
    /// </summary>
    /// <returns> the time before self destructing </returns>
    IEnumerator SelfDestruct()
    {
        //wait the self destruct time
        yield return new WaitForSeconds(timeToSelfDestruct);

        //apply the max amount of damage to the drone so the drone self destructs
        TakeDamage(maxHealth);
    }

    /// <summary>
    /// Returns the self destructed drone back to the pool of drones
    /// </summary>
    private void ReturnToPool()
    {
        //send this game object to the drone pool
        Pool.Release(this);
    }

    /// <summary>
    /// Resets the drone's values to default
    /// </summary>
    private void ResetDrone()
    {
        //set the drone's health back to their max health
        currentHealth = maxHealth;
    }

    /// <summary>
    /// FILLER CODE attacks the player
    /// </summary>
    private void AttackPlayer()
    {
        //debug out that the drone attack behaviors would be here
        Debug.Log("Attack player behaviors would go here");
    }

    /// <summary>
    /// Removes health from the drone
    /// </summary>
    /// <param name="amount"> the incoming damage to the drone </param>
    public void TakeDamage(float amount)
    {
        //remove the incoming damage from the drone's curren health
        currentHealth -= amount;

        //if the drone's current health is less than or equal to 0
        if(currentHealth <= 0.0f)
        {
            //return the drone back to the drone pool
            ReturnToPool();
        }
    }
}
