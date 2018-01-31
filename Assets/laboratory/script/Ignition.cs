using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignition : MonoBehaviour 
{
    public GameObject fireParticlePrefab;
    public bool startActive = false;

    private GameObject fireObject;

    public ParticleSystem customParticles;

    protected bool isBurning = false;

    public float burnTime;
    public float ignitionDelay = 0;

    private float ignitionTime;

    public AudioSource ignitionSound;

    public bool canSpreadFromThisSource = true;

    // Use this for initialization
    protected void Start()
    {
        if (startActive)
        {
            Invoke("StartBurning", ignitionDelay);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if ((burnTime != 0) && (Time.time > (ignitionTime + burnTime)) && isBurning)
        {
            isBurning = false;
            if (customParticles != null)
            {
                customParticles.Stop();
            }
            else
            {
                Destroy( fireObject );
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isBurning && canSpreadFromThisSource)
        {
            collider.SendMessageUpwards("FireExposure", SendMessageOptions.DontRequireReceiver);
        }
    }

    protected void FireExposure()
    {
        if (!isBurning)
        {
            Invoke("StartBurning", ignitionDelay);
        }
    }

    protected void StartBurning()
    {
        isBurning = true;
        ignitionTime = Time.time;

        // Play the fire ignition sound if there is one
        if (ignitionSound != null)
        {
            ignitionSound.Play();
        }

        if (customParticles != null)
        {
            customParticles.Play();
        }
        else
        {
            if (fireParticlePrefab != null)
            {
                fireObject = Instantiate(fireParticlePrefab, transform.position, transform.rotation) as GameObject;
                //fireObject.transform.localPosition.Set(0, (float)0.025, 0);
                fireObject.transform.parent = transform;
            }
        }
    }
}
