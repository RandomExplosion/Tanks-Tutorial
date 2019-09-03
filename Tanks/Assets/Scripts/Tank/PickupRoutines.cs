using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRoutines : MonoBehaviour
{

    public float ShroomEffectLength = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShroomEffect()
    {
        GetComponent<TankHealth>().m_StartingHealth = 200f; //Double the starting health
        GetComponent<TankHealth>().MultiplyHealth(2f); //Multiply the current health by 2

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        yield return new WaitForSeconds(ShroomEffectLength);
        GetComponent<TankHealth>().m_StartingHealth = 100f; //Double the starting health
        GetComponent<TankHealth>().DivideHealth(2f); //Multiply the current health by 2
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 5);
    }
}
