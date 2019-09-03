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

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.localScale += new Vector3(0.66f, 0.66f, 0.66f);
        Debug.Log("time starts now");
        yield return new WaitForSeconds(m_shroomEffectLength);
        Debug.Log("time's up");
        GetComponent<TankHealth>().m_StartingHealth = 100f; //Double the starting health
        GetComponent<TankHealth>().DivideHealth(2f);        //Multiply the current health by 2
        LerpReturnToOriginalSize();                         //Smootly Lerp to teh original Size
    }

    IEnumerator LerpReturnToOriginalSize()
    {
        Debug.Log("Returning to original size");
        float progress = 0;

        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), progress);
            progress += Time.deltaTime * Time.timeScale;
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return null;
    }
}
