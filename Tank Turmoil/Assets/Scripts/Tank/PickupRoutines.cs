using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRoutines : MonoBehaviour
{

    public float m_shroomEffectLength = 10f;
    public AudioSource m_sfxSource;

    public IEnumerator ShroomEffect()
    {
        GetComponent<TankHealth>().m_StartingHealth = 200f; //Double the starting health
        GetComponent<TankHealth>().MultiplyHealth(2f);      //Multiply the current health by 2
        m_sfxSource.Play();
        GetComponent<Rigidbody>().mass = 2;
        GetComponent<TankShooting>().m_MinLaunchForce *= 1.5f;
        GetComponent<TankShooting>().m_MaxLaunchForce *= 1.5f;
        GetComponent<TankShooting>().m_AimSlider.maxValue = GetComponent<TankShooting>().m_MaxLaunchForce;
        GetComponent<TankShooting>().m_AimSlider.minValue = GetComponent<TankShooting>().m_MinLaunchForce;

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.333f, 0.333f, 0.333f) /* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.333f, 0.333f, 0.333f)/* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.333f, 0.333f, 0.333f)/* - new Vector3(1, 1, 1)*/;
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f); //Make it exactly two
        //Debug.Log("time starts now");
        yield return new WaitForSeconds(m_shroomEffectLength);
        //Debug.Log("time's up");
        GetComponent<TankHealth>().m_StartingHealth = 100f; //Double the starting health
        GetComponent<TankHealth>().DivideHealth(2f);        //Divide the current health by 2
        GetComponent<Rigidbody>().mass = 1;
        GetComponent<TankShooting>().m_MinLaunchForce = 15;
        GetComponent<TankShooting>().m_MaxLaunchForce = 30;
        GetComponent<TankShooting>().m_AimSlider.maxValue = GetComponent<TankShooting>().m_MaxLaunchForce;
        GetComponent<TankShooting>().m_AimSlider.minValue = GetComponent<TankShooting>().m_MinLaunchForce;

        transform.localScale = new Vector3(1, 1, 1); //Return to the original size


        yield return null;
    }

}
