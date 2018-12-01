using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;

public enum MODE
{
    NONE,
    PLACE_GAME_SCENE,//first only
    PLACE_OBSTACLE,
    PLACE_TURRET_01,
    PLACE_BOMB,//gift  box

    _COUNT
}


public class GameController : MonoBehaviour {
    public GameObject Cursor;
    public GameObject sceneCenterPrefab;//where game center is going to be located
    GameObject sceneCenterObject;//scene center

    public GameObject enemy01Prefab,
                        turret01Prefab,
                          obstacle01Prefab,
                          bombPrefab;

    List<GameObject> enemies;

    MODE placeMode;

    bool levelStarted;

    void Start () {
        if (!Cursor)
            Debug.LogError("!Cursor");

    }
	
	void Update () {
        if (levelStarted)
            SpawnEnemy();
	}

    public void StartGame()
    {
        Debug.Log("GameController::StartGame");
        placeMode = MODE.PLACE_GAME_SCENE;
        levelStarted = false;

        enemies = new List<GameObject>();

        sceneCenterObject = Instantiate(sceneCenterPrefab, new Vector3(0, 0, 2), Quaternion.identity, this.transform);
        sceneCenterObject.GetComponent<TapToPlace>().StartPlacing();

    }

    //when tree is placed
    public void PlacingIsDone()
    {
        switch (placeMode)
        {
            case MODE.PLACE_GAME_SCENE:
                //lock the tree and scene
                Destroy(sceneCenterObject.GetComponent<TapToPlace>());
                StartLevel();
                break;
            case MODE.PLACE_OBSTACLE:
                placeMode = MODE.PLACE_OBSTACLE;
                GameObject obstacle = Instantiate(obstacle01Prefab, GetSpawnPos(), Quaternion.identity, this.transform);
                obstacle.GetComponent<TapToPlace>().StartPlacing();
                break;
            case MODE.PLACE_TURRET_01:
                break;
            case MODE.PLACE_BOMB:
                break;
            case MODE.NONE:
            case MODE._COUNT:
            default:
                break;
        }
    }

    public Vector3 GetSpawnPos()
    {
        return GameObject.FindGameObjectWithTag("SPAWN_PLACE").transform.position;
    }

    public void StartLevel()
    {
        levelStarted = true;
        placeMode = MODE.PLACE_OBSTACLE;
        GameObject obstacle = Instantiate(obstacle01Prefab, GetSpawnPos(), Quaternion.identity, this.transform);
        obstacle.GetComponent<TapToPlace>().StartPlacing();
    }

    void SpawnEnemy()
    {
        BoxCollider collider = sceneCenterObject.GetComponentInChildren<BoxCollider>();
        float distance = collider.bounds.size.x * 0.4f;//will spawn in radius of 90% space between center of scene and border.
        float angle = Random.Range(0, 360);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        enemies.Add(Instantiate(enemy01Prefab, new Vector3(x, collider.bounds.max.y, z), Quaternion.identity, sceneCenterObject.transform));
    }

    public void EndGame()//and restart
    {
        foreach (var e in enemies)
            Destroy(e);
        enemies.Clear();
    }

}


//click play to start the game
//click on screen to place the tree on mesh based on real world
//play