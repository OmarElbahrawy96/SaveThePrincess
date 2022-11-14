using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : MonoBehaviour {

    public float Timer = 5f;
    public GameObject enemy;
    public GameObject player;
    public GameObject checkPoint1;

    // Use this for initialization
    void Start () {
        enemy = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint1 = GameObject.FindGameObjectWithTag("check1");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (enemy == null)
        {
            Debug.Log("You Won");
        }
        if (player == null)
        {
            Debug.Log("GAME OVER");
        }

	}
}
