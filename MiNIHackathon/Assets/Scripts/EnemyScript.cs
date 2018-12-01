using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class EnemyScript : MonoBehaviour {

    // Use this for initialization
    public float maxHealth = 20.0f;
    public float health = 20.0f;

    public Image image;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void takeDamage(float damage)
    {
        health -= damage;

        image.fillAmount = health / maxHealth;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
