using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public int HPVille, ActualLife;
    public GameObject LifePoints, Effects;
    MenuController stat;

    // Start is called before the first frame update
    void Start()
    {
        stat = MenuController.instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        BoatController BateauScript = collision.gameObject.GetComponent<BoatController>();
        if (BateauScript != null)
        {
            string type = BateauScript.typeChar;

            if (BateauScript.typeChar == "B")
            {
                stat.SetState(MenuController.State.LOSE);
            }
            else
            {
                HPVille = HPVille - BateauScript.HPLife;
            }

            BateauScript.DoAction(this);

            if (HPVille < 10)
            {
                Effects.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (HPVille < 5)
            {
                Effects.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (HPVille <= 0)
            {
                stat.SetState(MenuController.State.LOSE);
            }
            else
            {
                for (int i = HPVille; i < ActualLife; i++)
                {
                    LifePoints.transform.GetChild(i).gameObject.SetActive(false);
                }

                ActualLife = HPVille;
            }
        }

    }

    public void InitCity()
    {
        Effects.transform.GetChild(0).gameObject.SetActive(false);
        Effects.transform.GetChild(1).gameObject.SetActive(false);
        HPVille = ActualLife = 20;
        for (int i = 0; i < LifePoints.transform.childCount; i++)
        {
            LifePoints.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
