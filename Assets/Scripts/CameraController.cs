using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Vector3 PrevPos;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
        bool TranslateButtonDown = Input.GetMouseButton(2);
        float MouseScroll = Input.mouseScrollDelta.y;

        Vector3 CurPos = Input.mousePosition;

        Vector3 Delta = PrevPos - CurPos;

        if (TranslateButtonDown)
        {
            this.gameObject.transform.Translate(Delta/20);
        }

        this.gameObject.transform.Translate(new Vector3(0, 0, MouseScroll));
        this.PrevPos = CurPos;
	}
}
