using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var light = this.GetComponent<Light>();
        light.range += Random.Range(-0.1f, 0.1f);

        if (light.range < 4.0f)
        {
            light.range = 4.0f;
        } else if (light.range > 5.0f)
        {
            light.range = 5.0f;
        }

        var x = this.transform;
        x.Translate(0, Random.Range(-0.005f, 0.005f), 0);
	}
}
