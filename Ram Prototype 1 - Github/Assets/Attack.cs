
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float meleeRate = 15f;


    public Transform MeleeStartPoint;
    public ParticleSystem hitParticle;
    public GameObject impactEffect;

    private float nextTimeToRam = 0f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToRam)
        {
            nextTimeToRam = Time.time + 1f / meleeRate;
            Shoot();
        }

        //DRAWS DEBUG RAY
        Debug.DrawRay(MeleeStartPoint.transform.position, MeleeStartPoint.transform.forward * range, Color.green);
        
    }

    void Shoot()
    {
        hitParticle.Play();


        RaycastHit hit;
        if (Physics.Raycast(MeleeStartPoint.transform.position, MeleeStartPoint.transform.forward, out hit, range))
        {
            print(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }



            GameObject Impacgog = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(Impacgog, 2f);

        }
    }


}
