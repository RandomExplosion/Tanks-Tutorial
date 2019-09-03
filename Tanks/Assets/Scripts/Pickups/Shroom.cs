using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : PickupBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPickup(GameObject player)
    {
        Debug.Log("Shroom at " + transform.position + " Picked up");
        player.GetComponent<PickupRoutines>().StartCoroutine("ShroomEffect");
        Destroy(gameObject);
    }
}
