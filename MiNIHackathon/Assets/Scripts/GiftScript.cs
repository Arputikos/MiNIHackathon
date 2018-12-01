using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class GiftScript : MonoBehaviour
{

    public float countdown = 5.0f;

    public float range = 10.0f;
    public float damage = 10.0f;

    public GameObject particlePrefab;

    public void Init(float _countdown, float _range, float _damage)
    {
        countdown = _countdown;
        range = _range;
        damage = _damage;
    }

	// Use this for initialization
	void Start () {
		//Init(6.0f, 10.0f, 10.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    countdown -= Time.deltaTime;
	    if (countdown <= 0)
	    {
	        Instantiate(particlePrefab, this.transform.position, Quaternion.identity,
	             this.transform.parent);

            ExplosionDamage(this.transform.position, range);

	        Destroy(gameObject);
            
        }
	}

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponentInParent<EnemyScript>())
            {
                hitColliders[i].GetComponentInParent<EnemyScript>().takeDamage(damage);
                hitColliders[i].SendMessage("AddDamage");
            }

            i++;
        }
    }


}
