using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public float Timer = 5f;
    public GameObject enemy;
    public GameObject player;
    public GameObject checkPoint1;
    public GameObject checkPoint2;
    public GameObject checkPoint3;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint1 = GameObject.FindGameObjectWithTag("checkPoint1");
        checkPoint2 = GameObject.FindGameObjectWithTag("checkPoint2");
        checkPoint3 = GameObject.FindGameObjectWithTag("checkPoint3");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (player.transform.position.x < checkPoint1.transform.position.x)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                Instantiate(enemy, checkPoint1.transform.position, checkPoint1.transform.rotation);
                Timer = 5f;
            }
            player.gameObject.GetComponent<Movement>().checkPointNo = 1;
        }
        if (player.transform.position.x > checkPoint1.transform.position.x && player.transform.position.x < checkPoint2.transform.position.x)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                Instantiate(enemy, checkPoint2.transform.position, checkPoint2.transform.rotation);
                Timer = 5f;
            }
            player.gameObject.GetComponent<Movement>().checkPointNo = 2;
        }
        if (player.transform.position.x > checkPoint1.transform.position.x && player.transform.position.x > checkPoint2.transform.position.x && player.transform.position.x < checkPoint3.transform.position.x)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                Instantiate(enemy, checkPoint3.transform.position, checkPoint3.transform.rotation);
                Timer = 5f;
            }
            player.gameObject.GetComponent<Movement>().checkPointNo = 3;
        }
    }
}
