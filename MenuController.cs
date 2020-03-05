using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum State
    {
        MENU,
        LEVEL,
        WIN,
        LOSE,
        CHEAT,
        CHEATYES,
        CHEATNO,
        GAME,
        CANT
    }

    public State state;

    public GameObject guiMenu;
    public GameObject guiLevel;
    public GameObject guiWin;
    public GameObject guiLose;
    public GameObject Game;
    public GameObject Cant;
    public GameObject Buttons;
    public GameObject Credits;
    public GameObject Controls;

    static public MenuController instance;  //singleton
    
    public int Level = 1;

    void Awake()
    {
        if (instance != null) Debug.LogError("Double singleton!");
        instance = this;
    }

    void Start()
    {
        state = State.MENU;
    }

    // Update is called once per frame
    void Update()
    {
        guiMenu.SetActive(state == State.MENU);
        guiLevel.SetActive(state == State.LEVEL || state == State.CANT);
        
        guiWin.SetActive(state == State.WIN);
        guiLose.SetActive(state == State.LOSE);
        Game.SetActive(state == State.GAME);
        
        Cant.SetActive(state == State.CANT);

        if (Level == 4)
        {
            guiWin.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            guiWin.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    public void OnClickPlay()
    {
        SetState(State.LEVEL);
        Debug.Log(state);
    }

    public void PageCredit(bool ToDisplay)
    {
        Buttons.SetActive(!ToDisplay);
        Credits.SetActive(ToDisplay);
    }

    public void PageControls(bool ToDisplay)
    {
        Buttons.SetActive(!ToDisplay);
        Controls.SetActive(ToDisplay);
    }

    public void OnClickMenu()
    {
        SetState(State.MENU);
    }

    public void OnClickRetry()
    {
        SetState(State.GAME);
        Game.GetComponent<GameController>().Init();
    }

    public void OnClickNext()
    {
        Level++;
        Game.GetComponent<GameController>().Init();
        SetState(State.GAME);
    }

    public void OnClickLevel(int nv)
    {
        if (Game.GetComponent<GameController>().MaxLevel >= nv)
        {
            Level = nv;
            Game.GetComponent<GameController>().Init();
            SetState(State.GAME);
        }
        else
        {
            SetState(State.CANT);
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickCantOK()
    {
        SetState(State.LEVEL);
    }
}
