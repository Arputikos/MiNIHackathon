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
    public GameObject popup;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void takeDamage(float damage)
    {
        health -= damage;

        image.fillAmount = health / maxHealth;

        Vector3 newPosition = transform.position + new Vector3(0, 1f, 0);
        var damagePopup = Instantiate(popup, newPosition, Quaternion.identity,
            transform);
        damagePopup.GetComponent<popupScript>().Init((-damage).ToString(),Color.red);


        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
