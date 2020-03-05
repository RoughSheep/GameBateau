using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbaleteController : MonoBehaviour
{
    //public GameObject Tourelle;
    public GameObject BoomPrefab, InstanceShoot;

    public float Power = 0;

    public float FireRate = 0;

    public bool IsShoot = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            Ray raymouse = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0f, 500.0f, 0f));

            transform.GetChild(0).rotation = Quaternion.Euler(-100 * raymouse.direction.y + 10.0f, 100*(raymouse.direction.x) - transform.position.x, 0.0f);

            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -8f)
            {
                transform.position = new Vector3((transform.position.x - Time.deltaTime * 15), transform.position.y, transform.position.z);
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 8f)
            {
                transform.position = new Vector3((transform.position.x + Time.deltaTime * 15), transform.position.y, transform.position.z);
            }

            if (!IsShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Power < 30)
                    {
                        Power++;
                    }
                }

                else if (Input.GetMouseButtonUp(0))
                {
                    // Create a bullet if hit
                    Vector3 vector3 = new Vector3(InstanceShoot.transform.position.x, InstanceShoot.transform.position.y, InstanceShoot.transform.position.z);
                    GameObject ball = Instantiate(BoomPrefab, vector3, InstanceShoot.transform.rotation);

                    // launch the ball
                    Vector3 direction = transform.GetChild(0).rotation * Vector3.forward;
                    ball.GetComponent<Rigidbody>().AddForce(direction * (1 + Power / 15) * 3000);
                    Power = 1;
                    IsShoot = true;
                }
            }
            else
            {
                FireRate += Time.deltaTime;
                if (FireRate > 1)
                {
                    IsShoot = false;
                    FireRate = 0;
                }
            }
        }            
    }
}