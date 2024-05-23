using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [04/16/2024]
 * [Object Pool of drone game objects]
 */

public class DroneObjectPool : MonoBehaviour
{
    //the max size of drones in the pool
    public int maxPoolSize = 10;

    //the default capacity of the drone pool
    public int stackDefaultCapacity = 10;

    //reference to the drone pool
    private IObjectPool<Drone> pool;

    //property for the drone pool that creates and initializes a new object pool of drones
    public IObjectPool<Drone> Pool
    {
        get
        {
            //if the pool is null
            if (pool == null)
            {
                //create a new pool of drones with a max number of drones
                pool = new ObjectPool<Drone>(CreatedPoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
            }

            //return the initialized pool of drones
            return pool;
        }
    }

    /// <summary>
    /// Creates a drone that is stored inside of the drone object pool
    /// </summary>
    /// <returns> a new drone game object </returns>
    private Drone CreatedPoolItem()
    {
        //generic variable for a new cube game object
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //initialize a drone as the cube game object
        Drone drone = go.AddComponent<Drone>();

        //name the drone, Drone
        drone.name = "Drone";

        //set the Pool of the drone to the created drone Pool
        drone.Pool = Pool;

        //return the new drone
        return drone;
    }

    /// <summary>
    /// When an object is returned to the drone object pool
    /// </summary>
    /// <param name="drone"> the drone being returned to the object pool </param>
    private void OnReturnedToPool(Drone drone)
    {
        //disable the drone
        drone.gameObject.SetActive(false);
    }

    /// <summary>
    /// When an object is taken/spawned in from the drone object pool
    /// </summary>
    /// <param name="drone"> the drone being taken/spawned in from the object pool </param>
    private void OnTakeFromPool(Drone drone)
    {
        //enable the drone
        drone.gameObject.SetActive(true);
    }

    /// <summary>
    /// When an object from the drone object pool is destroyed
    /// </summary>
    /// <param name="drone"> the drone being destroyed from the drone object pool </param>
    private void OnDestroyPoolObject(Drone drone)
    {
        //destroy the drone
        Destroy(drone);
    }

    /// <summary>
    /// Spawns one of the drone objects from the object pool
    /// </summary>
    public void Spawn()
    {
        //generic variable for a random amount of drones in the object pool
        var amount = Random.Range(1, 10);

        for (int index = 0; index < amount; index++)
        {
            //generic variable for a drone out of the drone object pool
            var drone = Pool.Get();

            //set the drone's position to a random spot within a unit sphere distance
            drone.transform.position = Random.insideUnitSphere * 10;
        }
    }
}
