using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrunkController : MonoBehaviour {

    private float scalingFactor = 1.0f;

    private Text EnergyText;
    private Text HarvestText;
    public Text EndGameScreenText;
    private GameObject EndGameScreen;

    private int _DaysUntilHarvest = 10;
    public int DaysUntilHarvest
    {
        get
        {
            return _DaysUntilHarvest;
        }
        set
        {
            _DaysUntilHarvest = value;
            if (_DaysUntilHarvest < 0) _DaysUntilHarvest = 0;
            HarvestText.text = "Days Until Harvest: " + _DaysUntilHarvest;
        }
    }

    private int _Energy = 2;
    public int Energy {
        get
        {
            return _Energy;
        }
        set
        {
            _Energy = value;
            EnergyText.text = "Energy: " + _Energy;
        }
    }

    public void ProcessDay()
    {
        FruitController[] fruitList = this.gameObject.transform.parent.GetComponentsInChildren<FruitController>();
        foreach (FruitController fc in fruitList)
        {
            if (this.Energy < 5)
            {
                Destroy(fc.gameObject);
                this.Energy = 0;
            }
            else
            {
                this.Energy -= 5;
            }

        }
        LeafController[] leafList = this.gameObject.transform.parent.GetComponentsInChildren<LeafController>();

        this.Energy += leafList.Length;

        this.DaysUntilHarvest -= 1;

        if (this.DaysUntilHarvest == 0)
        {
            this.EndGame();
        }
    }

    public void EndGame() {
        FruitController[] fruitList = this.gameObject.transform.parent.GetComponentsInChildren<FruitController>();
        this.EndGameScreen.SetActive(true);
        this.EndGameScreenText.GetComponent<Text>().text = "Harvest Time!\nYou collected " + fruitList.Length + " fruit!\nTry again to get a better score!";
    }

	// Use this for initialization
	void Start () {
        this.EnergyText = GameObject.Find("Energy UI").GetComponent<Text>();
        this.HarvestText = GameObject.Find("Harvest UI").GetComponent<Text>();
        this.EndGameScreen = GameObject.Find("End Game Screen").gameObject;

        ResetGame();
    }

    GameObject makeNewBranch(string name, Vector3 position, Vector3 rotation)
    {
        Debug.Log(GameObject.Find("DefaultBranch"));
        GameObject go = Instantiate(GameObject.Find("DefaultBranch"));
        SetLayerRecursive(go, 0);
        go.tag = "Dynamic";
        go.name = name;
        go.transform.parent = this.transform.parent;
        go.transform.localPosition = position;
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = rotation;
        go.transform.localRotation = newRot;
        return go;
    }

    void SetLayerRecursive(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    public void ResetGame()
    {
        this.EndGameScreen.SetActive(false);
        this.DaysUntilHarvest = 15;
        this.Energy = 2;
        GameObject[] dynamicObjects = GameObject.FindGameObjectsWithTag("Dynamic");
        foreach(GameObject d in dynamicObjects)
        {
            Destroy(d);
        }
        this.makeNewBranch("Right", new Vector3(0, 1.8f, 0.425f), new Vector3(25, 0, 0));
        this.makeNewBranch("Left", new Vector3(0, 1.8f, -0.425f), new Vector3(-25, 0, 0));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
