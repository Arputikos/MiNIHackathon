using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour {

    ParticleSystem system;
    float timeDead = 0;

	void Awake () {
        system = this.GetComponent<ParticleSystem>();
        if (!system)
            Debug.LogError("This is not particle object! Missing particle component");

        Debug.Log("avake");
        //gameCtrl = GameObject.FindGameObjectWithTag("scripts").GetComponent<gameController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (system.particleCount <= 0 && timeDead > 0f)//
        {
            Debug.Log("destroying");
            Destroy(this.gameObject);//WARNING can cause issues. If so, use DestroyImmediate 
        }

        timeDead += Time.deltaTime;

        //if (gameController.i.gameState != gameController.GAME_STATE.GAME)
        //    return;

        //if (gameController.i.ingameState == gameController.INGAME_STATE.PAUSE && !system.isPaused)
        //    system.Pause();
        //else if(gameController.i.ingameState == gameController.INGAME_STATE.GAME && system.isPaused)
        //    system.Play();
    }

}
