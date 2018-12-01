using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScript : MonoBehaviour {

    public float range = 20.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    Collider[] enemiesColliders = Physics.OverlapSphere(this.transform.position, range);
	    if (enemiesColliders.Length > 0)
	    {
	        Vector3 dir = enemiesColliders[0].transform.position - transform.position;
	        Quaternion lookRot = Quaternion.LookRotation(dir);
	       
            transform.localRotation = Quaternion.Euler(lookRot.eulerAngles.x, 0, 0);
        }
    }
}
