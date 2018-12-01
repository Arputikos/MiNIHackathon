using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    // Use this for initialization
    public float maxHealth = 20.0f;
    public float health = 20.0f;

    public Image image;
    public GameObject popup;
    NavMeshAgent agent;

    bool dead = false;
    float timeDead = 5;

    Vector3 destination;

	void Awake () {
        agent = GetComponent<NavMeshAgent>();
        //align to closest point on mesh
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(transform.position, out myNavHit, 100, -1))
        {
            transform.position = myNavHit.position;
        }
        destination = GameObject.FindGameObjectWithTag("TREE").transform.position;
        agent.SetDestination(destination);
        this.transform.LookAt(destination);
	}
	
	// Update is called once per frame
	void Update () {
        if ((this.transform.position - destination).magnitude< 0.4f && !dead && !GetComponent<Animator>().GetBool("Attack"))
        {
            GetComponent<Animator>().SetBool("Attack", true);
            agent.Stop();
        }
        if (dead)
        {
            if (timeDead <= 0)
                Destroy(this.gameObject);
            timeDead -= Time.deltaTime;
        }

	}


    public void takeDamage(float damage)
    {
        health -= damage;

        image.fillAmount = health / maxHealth;

        Vector3 newPosition = transform.position + new Vector3(0, 1f, 0);
        var damagePopup = Instantiate(popup, newPosition, Quaternion.identity,
            transform);
        damagePopup.GetComponent<popupScript>().Init((-damage).ToString(),Color.red);


        if (health <= 0)
        {
            //Destroy(gameObject);
            GetComponent<Animator>().SetBool("Death", true);
            dead = true;
        }
    }
}
