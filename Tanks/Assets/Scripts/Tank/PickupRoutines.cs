using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRoutines : MonoBehaviour
{

    public float m_shroomEffectLength = 10f;
    public AudioSource m_sfxSource;

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
        GetComponent<TankHealth>().MultiplyHealth(2f);      //Multiply the current health by 2
        m_sfxSource.Play();
        GetComponent<Rigidbody>().mass = 3;
        GetComponent<TankShooting>().m_CurrentLaunchForce *= 3;

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3)/3/* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3) / 3/* - new Vector3(1, 1, 1)*/;
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(3, 3, 3) / 3/* - new Vector3(1, 1, 1)*/;
        Debug.Log("time starts now");
        yield return new WaitForSeconds(m_shroomEffectLength);
        Debug.Log("time's up");
        GetComponent<TankHealth>().m_StartingHealth = 100f; //Double the starting health
        GetComponent<TankHealth>().DivideHealth(2f);        //Divide the current health by 2
        GetComponent<Rigidbody>().mass = 1;
        //LerpReturnToOriginalSize();                         //Smootly Lerp to teh original Size
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
