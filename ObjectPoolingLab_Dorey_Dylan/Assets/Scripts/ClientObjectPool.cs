using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [04/16/2024]
 * [The client component that allows the user to interact with the demo]
 */

public class ClientObjectPool : MonoBehaviour
{
    //reference to the drone object pool
    private DroneObjectPool pool;

    private void Start()
    {
        //initialize and add a drone object pool this gameobject
        pool = gameObject.AddComponent<DroneObjectPool>();
    }

    /// <summary>
    /// TESTING PURPOSES ONLY ( DO NOT USE IN PRODUCTION CODE ) EXTREMELY INEFFICIENT
    /// </summary>
    private void OnGUI()
    {
        //create a spawn drone button
        if(GUILayout.Button("Spawn Drones"))
        {
            //if pressed, spawn a drone from the drone object pool
            pool.Spawn();
        }
    }
}
