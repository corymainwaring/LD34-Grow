using UnityEngine;
using System.Collections;

public class LeafController : MonoBehaviour {

    public Transform PivotPoint;
    public TrunkController Trunk;
    public int level;

	// Use this for initialization
	void Start () {
        this.Trunk = GameObject.Find("Trunk").GetComponent<TrunkController>();
	}

    void SetLayerRecursive(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    GameObject makeNewBranch(string name, Vector3 position, Vector3 rotation)
    {
        GameObject go = Instantiate(GameObject.Find("DefaultBranch"));
        SetLayerRecursive(go, 0);
        go.tag = "Dynamic";
        go.name = name + this.level;
        go.transform.parent = this.transform.parent;
        go.transform.localPosition = position;
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = rotation;
        go.transform.localRotation = newRot;
        go.GetComponentInChildren<LeafController>().level = this.level + 1;
        //Trunk.Grow();
        return go;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (this.Trunk.Energy >= 2)
            {
                this.makeNewBranch("Right", new Vector3(0, 1.8f, 0.425f), new Vector3(25, 0, 0));
                this.makeNewBranch("Left", new Vector3(0, 1.8f, -0.425f), new Vector3(-25, 0, 0));
                Destroy(this.gameObject);
                this.Trunk.Energy -= 2;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (this.Trunk.Energy >= 10)
            {
                Transform parentTransform = this.transform.parent;
                GameObject Fruit = parentTransform.Find("Fruit").gameObject;
                Fruit.SetActive(true);
                Destroy(this.gameObject);
                this.Trunk.Energy -= 10;
            }
        }
    }
}
