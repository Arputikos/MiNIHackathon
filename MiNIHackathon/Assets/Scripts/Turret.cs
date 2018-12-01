using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 20.0f;
	public Collider target;
	public int damage = 1;

	float attackTimer = 0;
	public float attackSpeed = 1;


	float shootTimer = 0;
	public float shootTime = 1;
	bool shooting;

	public LineRenderer[] lr;
	public Transform[] rayPos;

	int r;
	// Use this for initialization
	void Start () {
		
		lr[0].enabled = false;
		lr[1].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] enemiesColliders = Physics.OverlapSphere(this.transform.position, range);




		for (int i = 0; i < enemiesColliders.Length; i++)
		{
			if (enemiesColliders[i].tag == "Enemy" && !enemiesColliders[i].gameObject.GetComponent<EnemyScript>().dead)
			{
				target = enemiesColliders[i];
				break;
			}
		}


		if (target != null)
		{
			Vector3 dir = target.transform.position - new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
			Quaternion lookRot = Quaternion.LookRotation(dir);

			transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
			transform.GetChild(0).GetChild(0).GetChild(0).localRotation = Quaternion.Euler(lookRot.eulerAngles.x, 0, 0);
		}

		attackTimer += attackSpeed * Time.deltaTime;

		if(attackTimer > 10)
		{
			if(target != null)
			{
				target.transform.GetComponent<EnemyScript>().takeDamage(damage);
				shooting = true;


				r = Random.Range(0, 2);
				lr[r].SetPosition(0, rayPos[r].position);
				lr[r].SetPosition(1, target.transform.position);
				lr[r].enabled = true;

				attackTimer = 0;
				target = null;

			}



		}

		if (shooting)
		{
			shootTimer += shootTime * Time.deltaTime;
			if (shootTimer > 10)
			{
				shootTimer = 0;
				shooting = false;
				lr[r].enabled = false;
			}
		}
		

	}
}
