using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour{

	[SerializeField]
    private float waitTime;

    // Update is called once per frame
    void Update()
    {
        waitTime -= 1;
        if (waitTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}