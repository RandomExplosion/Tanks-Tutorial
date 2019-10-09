using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : PickupBehaviorBase
{
  

    public override void OnPickup(GameObject player)
    {
        Debug.Log("Shroom at " + transform.position + " Picked up");
        player.GetComponent<PickupRoutines>().StartCoroutine("ShroomEffect"); //Call ShroomEffect on the tank
        Destroy(gameObject);
    }
}
