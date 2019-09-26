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
        GetComponent<Rigidbody>().mass = 3;
        GetComponent<TankShooting>().m_MinLaunchForce *= 2;
        GetComponent<TankShooting>().m_MaxLaunchForce *= 2;
        GetComponent<TankShooting>().m_AimSlider.maxValue = GetComponent<TankShooting>().m_MaxLaunchForce;
        GetComponent<TankShooting>().m_AimSlider.minValue = GetComponent<TankShooting>().m_MinLaunchForce;

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3)/3/* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3) / 3/* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3) / 3/* - new Vector3(1, 1, 1)*/;
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
        //LerpReturnToOriginalSize();                       //Smootly Lerp to teh original Size
        transform.localScale = new Vector3(1, 1, 1);

        yield return null;
    }

    IEnumerator LerpReturnToOriginalSize()
    {
        Debug.Log("Returning to original size");
        float progress = 0;

        while (progress <= 100)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), progress);
            progress += Time.deltaTime * Time.timeScale;
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return null;
    }


}
