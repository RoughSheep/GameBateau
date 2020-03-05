using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    // Start is called before the first frame update
    public string typeChar = "A";
    public int HPLife = 0;
    public float Speed;
    public GameObject LifePoints;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
        
        if (HPLife == 0)
        {
            Destroy(gameObject);
        }
    }

    public void DoAction(CityController ville)
    {
        Destroy(gameObject);
    }

    public void LoseLife()
    {
        LifePoints.transform.GetChild(HPLife).gameObject.SetActive(false);
    }
}
