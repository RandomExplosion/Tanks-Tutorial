using UnityEngine;
using UnityEngine.UI;
using Pooling;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              //Used to identify the different players.
    public Rigidbody m_Shell;                   //Prefab of the shell.
    public Transform m_FireTransform;           //A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  //A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         //Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            //Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                //Audio that plays when each shot is fired.
    public float m_MinLaunchForce = 15f;        //The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 30f;        //The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       //How long the shell can charge for before it is fired at max force.
    public float m_FireCooldown = 1.5f;         //How long the player has to wait between firing and begining to charge again.
    public float m_CurrentLaunchForce;          //The force that will be given to the shell when the fire button is released.
    public float m_MinNextFireTime;

    private string m_FireButton;                //The input axis that is used for launching shells.
    private float m_ChargeSpeed;                //How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       //Whether or not the shell has been launched with this button press.
    private int m_NextFireTime;                 //The time when the player can next fire
    private bool m_FiredEver;                   //Whether the player has pressed the fire button at all this round

    private void OnEnable()
    {
        //When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }

    private void Start()
    {
        //The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;

        //The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

        //Default the min time to now
        m_MinNextFireTime = Time.time;
    }


    private void Update()
    {
        //The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;
        
        //If the cooldown has finished
        if (Time.time > m_MinNextFireTime)
        {
            //If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                //... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            //Otherwise, if the fire button has just started being pressed...
            else if (Input.GetButtonDown(m_FireButton))
            {
                //... reset the fired flag and reset the launch force.
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                //Change the clip to the charging clip and start it playing.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            //Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                //Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            //Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                //... launch the shell.
                Fire();
            } 
        }
    }


    private void Fire()
    {
        //Set the fired flag so only Fire is only called once.
        m_Fired = true;

        //Calculate the time when the cooldown will be completed
        m_MinNextFireTime = Time.time + m_FireCooldown;

        //Take an instance of the shell from the object pool and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            ObjectPooler.Instance.SpawnFromPool("shellpool" + m_PlayerNumber, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();

        if (transform.localScale.x > 1f || transform.localScale.y > 1f || transform.localScale.z > 1f) //If the scale isn't 1, 1, 1
        {
            shellInstance.transform.GetComponent<ShellExplosion>().m_ExplosionRadius = 15; //Multiply the explosion Radius by 3
            shellInstance.transform.GetComponent<ShellExplosion>().m_MaxDamage = 300; //multiply the max damage by 2
            shellInstance.transform.localScale = new Vector3(3, 3, 3); //multiply the localscale by 3
            shellInstance.GetComponent<Rigidbody>().mass = 3f;
        }
       
        //Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        //Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}
