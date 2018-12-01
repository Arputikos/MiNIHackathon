using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;

public class GameController : MonoBehaviour {
    public GameObject Cursor;
    public GameObject treePrefab;//where game center is going to be located
    public GameObject sceneCenterObject;//scene center

    void Start () {
        if (!Cursor)
            Debug.LogError("!Cursor");

    }
	
	void Update () {
		
	}

    public void StartGame()
    {
        Debug.Log("GameController::StartGame");

        sceneCenterObject = Instantiate(treePrefab, new Vector3(0, 0, 2), Quaternion.identity, this.transform);
        sceneCenterObject.GetComponent<TapToPlace>().StartPlacing();

    }

    //when tree is placed
    public void ScenePlaced()
    {
        Destroy(sceneCenterObject.GetComponent<TapToPlace>());
    }

    public void StartLevel()
    {

    }

    public void EndGame()//and restart
    {

    }

}


//click play to start the game
//click on screen to place the tree on mesh based on real world
//play