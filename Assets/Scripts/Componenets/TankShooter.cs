using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;
    public GameObject shellProjectile;
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    public override void Shoot(GameObject shellProjectile, float fireForce, float damageDone, float lifespan)
    {
        MakeNoise(50);
        GameObject newShell = Instantiate(shellProjectile, firepointTransform.position, firepointTransform.rotation) as GameObject;
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();

        if (damageOnHit != null)
        {
            damageOnHit.damageDone = damageDone;
            damageOnHit.owner = GetComponent<Pawn>();
        }
        Rigidbody rbody = newShell.GetComponent<Rigidbody>();

        if (rbody != null)
        {
            rbody.AddForce(firepointTransform.forward * fireForce);
        }
        Destroy(newShell, lifespan);
    }
    public override void MakeNoise(float shootVolume)
    {
        NoiseMaker noiseMaker = gameObject.GetComponent<NoiseMaker>();
        noiseMaker.volumeDistance = shootVolume;
        if (noiseMaker.volumeDistance > 0)
        {
            noiseMaker.volumeDistance = 0;
        }
    }
}
