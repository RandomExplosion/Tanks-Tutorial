using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{

    private void OnTriggerEnter(Collider intruder)
    {
       // if (intruder.gameObject.layer.ToString() == "Players")
       // {
            OnPickup(intruder.gameObject);
       // }
    }

    public virtual void OnPickup(GameObject player)
    {

    }
}
