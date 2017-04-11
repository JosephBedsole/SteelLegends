using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManger : MonoBehaviour {
    public static ParticleManger instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
