using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour {
    static bool DEBUG = false;

    public GameController gameController;
    public GameMenu gameStartMenu;
    
	// Use this for initialization
	void Start () {
        if (DEBUG)
        {
            gameController.StartGame();
            gameStartMenu.Hide();
        }

        if (!gameController)
        {
            Debug.LogError("!gameController");
        }
        if (!gameStartMenu)
        {
            Debug.LogError("!gameMenu");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartClick()
    {
        Debug.Log("GameView::StartClick");

        HideMenu();
        gameController.StartGame();
    }

    public void ShowMenu()
    {
        gameStartMenu.Show();
    }

    public void HideMenu()
    {
        gameStartMenu.Hide();
    }
}
