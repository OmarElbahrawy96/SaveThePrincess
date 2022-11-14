using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().takeSpikeDamage();
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().takeDamage(1000f);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
