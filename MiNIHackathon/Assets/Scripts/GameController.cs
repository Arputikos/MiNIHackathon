using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GameController : MonoBehaviour {
    public GameObject Cursor;

    public Spawnable spawnObjectPrefab;

    private bool IsGravity = false;

    // Use this for initialization
    void Start () {
        if (!Cursor)
        {
            Debug.LogError("!Cursor");
        }
        if (!spawnObjectPrefab)
        {
            Debug.LogError("!spawnObjectPrefab");
        }

        //GravityOff();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        Debug.Log("GameController::StartGame");

        SpawnObject();
    }

    public void SpawnObject()
    {
        Spawn(spawnObjectPrefab, new Vector3(0, 0, 2), Quaternion.identity);
    }

    public void Spawn(
    Spawnable spawnObjectPrefab,
    Vector3 position,
    Quaternion rotation)
    {
        Debug.Log("Spawner::Spawn");
        var spawnedObject = Instantiate(spawnObjectPrefab, position, rotation, null);

        SpawnInit(spawnedObject);
    }

    private void SpawnInit(Spawnable spawnedObject)
    {
        spawnedObject.Init(Cursor);
        var rigidbody = spawnedObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.isKinematic = !IsGravity;
        }
    }

    public void ToggleGravity()
    {
        if (IsGravity)
        {
            GravityOff();
        }
        else
        {
            GravityOn();
        }
    }

    public void GravityOn()
    {
        IsGravity = true;

        Debug.Log("Spawner::GravityOn");

        var foundObjects = FindObjectsOfType<Rigidbody>();
        foreach(var foundObject in foundObjects)
        {
            foundObject.isKinematic = false;
        }
    }

    public void GravityOff()
    {
        IsGravity = false;

        Debug.Log("Spawner::GravityOff");

        var foundObjects = FindObjectsOfType<Rigidbody>();
        foreach (var foundObject in foundObjects)
        {
            foundObject.isKinematic = true;
        }
    }
}
