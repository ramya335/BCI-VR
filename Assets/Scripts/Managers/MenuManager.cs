// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    static MenuManager instance;

    [SerializeField]
    GameObject mainMenuObject;
    Canvas mainMenuCanvas;
    bool mainMenuVisible = true;

    [SerializeField]
    GameObject bciMenuCanvas;

    [SerializeField]
    GameObject optionsMenuCanvas;

    [SerializeField]
    GameObject controlsMenuCanvas;

    [SerializeField]
    GameObject endGameMenuCanvas;

    public static Action menuOpen;
    public static Action menuClosed;
    public static Action newGame;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            menuOpen?.Invoke();
            mainMenuCanvas = mainMenuObject.GetComponent<Canvas>();
        }
    }

    public void onOpen(InputAction.CallbackContext ctx)
    {
        //If button pressed down, open menu.
        if (ctx.performed)
        {
            openMenu();
        }
    }

    void openMenu()
    {
        if (mainMenuVisible == false)
        {
            mainMenuCanvas.enabled = true;
            mainMenuVisible = true;
            menuOpen?.Invoke();
        }
    }

    // ################ Main Menu ################

    void resume()
    {
        // Hide Main Menu
        mainMenuCanvas.enabled = false;
        mainMenuVisible = false;
        menuClosed?.Invoke();
    }

    void openBCI()
    {
        //Swap menus
        mainMenuCanvas.enabled = false;
        mainMenuVisible = false;
        bciMenuCanvas.SetActive(true);
    }

    void openOptions()
    {
        mainMenuCanvas.enabled = false;
        mainMenuVisible = false;
        optionsMenuCanvas.SetActive(true);
    }

    void openControls()
    {
        mainMenuCanvas.enabled = false;
        mainMenuVisible = false;
        controlsMenuCanvas.SetActive(true);
    }

    void quitGame()
    {
        // Only works once built for release.
        Application.Quit();
    }

    void mainBack()
    {
        mainMenuCanvas.enabled = true;
        mainMenuVisible = true;
    }

    // ################ BCI Menu ################

    // Enable and Disable BCI is handled by BCI Manager.

    void backBCI()
    {
        //Swap menus
        bciMenuCanvas.SetActive(false);
        mainMenuCanvas.enabled = true;
        mainMenuVisible = true;
    }

    // ################ Options Menu ################


    void backOptions()
    {
        //Swap menus
        optionsMenuCanvas.SetActive(false);
        mainMenuCanvas.enabled = true;
        mainMenuVisible = true;
    }

    // ################ Controls Menu ################
    void backControls()
    {
        controlsMenuCanvas.SetActive(false);
        mainMenuCanvas.enabled = true;
        mainMenuVisible = true;
    }

    // ################ Control Flow ################
    void endGameOpen()
    {
        mainMenuCanvas.enabled = false;
        mainMenuVisible = false;
        optionsMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        bciMenuCanvas.SetActive(false);

        // Enable End Game Menu
        Debug.Log("End Game Menu Open");
        endGameMenuCanvas.SetActive(true);
    }

    // ################ End Game Menu ################
    void newGameStart()
    {
        // Restart Game
        newGame?.Invoke();
        Debug.Log("End Game Menu Closed.");
        endGameMenuCanvas.SetActive(false);
        Debug.Log("Main Menu Open.");
        mainMenuCanvas.enabled = true;
        menuOpen?.Invoke();
    }

    // Subscribe
    private void OnEnable()
    {
        MainMenu.resumeClicked += resume;
        MainMenu.bciClicked += openBCI;
        MainMenu.optionsClicked += openOptions;
        MainMenu.controlsClicked += openControls;
        MainMenu.quitClicked += quitGame;

        NewBCIMenu.backClicked += backBCI;
            
        OptionsMenu.backClicked += backOptions;

        ControlsMenu.backClicked += backControls;

        EventEnd.endGame += endGameOpen;
        EndGameMenu.newGameClicked += newGameStart;
        EndGameMenu.quitClicked += quitGame;
    }

    // Unsubscribe
    private void OnDisable()
    {
        MainMenu.resumeClicked -= resume;
        MainMenu.bciClicked -= openBCI;
        MainMenu.optionsClicked -= openOptions;
        MainMenu.controlsClicked -= openControls;
        MainMenu.quitClicked -= quitGame;

        NewBCIMenu.backClicked -= backBCI;

        OptionsMenu.backClicked -= backOptions;

        ControlsMenu.backClicked -= backControls;

        EventEnd.endGame -= endGameOpen;
        EndGameMenu.newGameClicked -= newGameStart;
        EndGameMenu.quitClicked -= quitGame;
    }
}
