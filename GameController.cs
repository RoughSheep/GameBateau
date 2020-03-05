using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static public MenuController state;

    public BoatController[] BateauPrefabs;

    public GameObject Ships, Ville, PausePage;
    
    public int currentLevel = 0;
    public int MaxLevel = 1;

    public bool IsPlaying = true;

    void Awake()
    {
        state = MenuController.instance;
    }

    void Start()
    {
        state = MenuController.instance;
        
        Init();
    }

    public void Init()
    {
        PausePage.transform.gameObject.SetActive(false);
        state = MenuController.instance;
        currentLevel = state.Level;
        for (int i = 0; i < Ships.transform.childCount; i++)
        {
            Destroy(Ships.transform.GetChild(i).gameObject);
        }
        
        Ville.GetComponent<CityController>().InitCity();
        LoadLevel();
        IsPlaying = true;
    }

    public void LoadLevel()
    {
        TextAsset txt = (TextAsset)Resources.Load("Niveau" + currentLevel.ToString("00"), typeof(TextAsset));

        if (txt == null)
        {
            Debug.LogError("Connot find file in Resources: " + "level" + currentLevel.ToString("00"));
        }
        else
        {
            CreateLevel(txt.text);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PagePause();
        }

        if (IsPlaying && Ville.GetComponent<CityController>().HPVille > 0 && Ships.transform.childCount == 0)
        {
            state.SetState(MenuController.State.WIN);

            if (currentLevel < 4)
            {
                currentLevel++;
                Debug.Log(currentLevel);
            }

            if (MaxLevel<currentLevel)
            {
                MaxLevel=currentLevel;
                Debug.Log(MaxLevel);
            }


            IsPlaying = false;
            gameObject.SetActive(false);
        }
    }

    public void CreateLevel(string levelString)
    {
        int x = -40;
        int z = 0;

        foreach (char element in levelString)
        {
            if (element == ' ')
            {
                z = z + 10;
            }
            else if (element == '\n')
            {
                x = x + 15;
                z = 0;
            }
            else
            {
                CreatBateau(element.ToString(), new Vector3(x, -6, z));
                z = z + 10;
            }
        }
    }

    void CreatBateau(string BateauType, Vector3 position)
    {
        BoatController myPrefab = null;
        foreach (BoatController prefab in BateauPrefabs)
        {
            if (prefab.typeChar == BateauType)
            {
                myPrefab = prefab;
            }
        }

        if (myPrefab != null)
        {
            BoatController newBateau = Instantiate<BoatController>(myPrefab, position, Quaternion.identity, Ships.transform);
        }

    }

    public void PagePause()
    {
        IsPlaying = !IsPlaying;
        PausePage.transform.gameObject.SetActive(!IsPlaying);
        gameObject.SetActive(false);
        
        if (IsPlaying)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        gameObject.SetActive(true);
    }

    public void Quit()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        state.SetState(MenuController.State.MENU);
    }
}
