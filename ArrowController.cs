using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject HitBoat, HitWater;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Arrow")
        {
            GameObject Effect;
            if (collision.gameObject.tag == "Ennemis")
            {
                BoatController BateauScript = collision.gameObject.GetComponent<BoatController>();
                BateauScript.HPLife--;
                BateauScript.LoseLife();
                Effect = Instantiate(HitBoat, transform.position, new Quaternion());
                Destroy(Effect, 0.4f);
            }
            else if (collision.gameObject.tag == "Ocean")
            {
                Effect = Instantiate(HitWater, transform.position, new Quaternion());
                Destroy(Effect, 2f);
            }

            Destroy(gameObject);
        }
    }
}
