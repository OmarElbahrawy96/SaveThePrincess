using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigate : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position + offset;
	}
}
