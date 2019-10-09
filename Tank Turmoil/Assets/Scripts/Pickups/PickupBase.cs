using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupBehaviorBase : MonoBehaviour
{

    private void OnTriggerEnter(Collider intruder)
    {
       // if (intruder.gameObject.layer.ToString() == "Players")
       // {
            OnPickup(intruder.gameObject); //Call onpickup
       // }
    }

    public virtual void OnPickup(GameObject player)
    {

    }
}
