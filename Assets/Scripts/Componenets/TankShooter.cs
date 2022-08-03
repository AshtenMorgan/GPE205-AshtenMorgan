using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public Transform firepointTransform;
    public GameObject shellProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(GameObject shellProjectile, float fireForce, float damageDone, float lifespan)
    {
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
}
