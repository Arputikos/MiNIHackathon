using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.AI;

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

    float timeToNextSpawn;//spawning every random time
    float spawnTime;

    void Start () {
        if (!Cursor)
            Debug.LogError("!Cursor");

        var x = GameObject.FindGameObjectsWithTag("gameScene");
        foreach (var y in x)
            Destroy(y);

    }
	
	void Update () {
        //spawning enemies every random time
        if (levelStarted && spawnTime >= timeToNextSpawn)
        {
            SpawnEnemy();
            spawnTime = 0;
        }
        spawnTime += Time.deltaTime;
	}

    public void StartGame()
    {
        Debug.Log("GameController::StartGame");
        placeMode = MODE.PLACE_GAME_SCENE;
        levelStarted = false;
        spawnTime = 0;
        timeToNextSpawn = 1.0f;

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
                placeMode = MODE.PLACE_BOMB;
                GameObject obstacle = Instantiate(bombPrefab, GetSpawnPos(), Quaternion.identity, this.transform);
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

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;

            // Do something with the object that was hit by the raycast.
        }
        return Camera.main.gameObject.transform.position;//awaryjnie
         */
        
    }

    public void StartLevel()
    {
        //biold navmesh
        sceneCenterObject.GetComponent<NavMeshSurface>().BuildNavMesh();   

        levelStarted = true;
        spawnTime = 0;
        timeToNextSpawn = Random.Range(0f, 2.0f);
        placeMode = MODE.PLACE_BOMB;
        GameObject obstacle = Instantiate(bombPrefab, GetSpawnPos(), Quaternion.identity, this.transform);
        obstacle.GetComponent<TapToPlace>().StartPlacing();
    }

    void SpawnEnemy()
    {
        BoxCollider collider = sceneCenterObject.GetComponentInChildren<BoxCollider>();
        float distance = collider.bounds.size.x * 0.4f;//will spawn in radius of 90% space between center of scene and border.
        float angle = Random.Range(0, 360);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance + sceneCenterObject.transform.position.x;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance + sceneCenterObject.transform.position.z;

        enemies.Add(Instantiate(enemy01Prefab, new Vector3(x, sceneCenterObject.transform.position.y, z), Quaternion.identity, GameObject.FindGameObjectWithTag("INSTANTIATE_CONTAINER").transform));
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