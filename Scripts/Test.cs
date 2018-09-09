using UnityEngine;

public class Test : MonoBehaviour
{
	void Start()
	{
		EMT.Detector detector = GetComponent<EMT.Detector>();

		detector.OnLayerEnter("Default", this.TestEnterLayerMethod);
		detector.OnLayerStay("Default", this.TestStayLayerMethod);
		detector.OnLayerExit("Water", this.TestExitLayerMethod);

		detector.OnTagEnter("Untagged", this.TestEnterTagMethod);
		detector.OnTagStay("Untagged", this.TestStayTagMethod);
		detector.OnTagExit("Untagged", this.TestExitTagMethod);
	}

	void TestEnterLayerMethod(GameObject go)
	{
		Debug.Log("Enter layer: " + go.name);
	}

	void TestStayLayerMethod(GameObject go)
	{
		Debug.Log("Stay layer: " + go.name);
	}

	void TestExitLayerMethod(GameObject go)
	{
		Debug.Log("Exit layer: " + go.name);
	}

	void TestEnterTagMethod(GameObject go)
	{
		Debug.Log("Enter tag: " + go.name);
	}

	void TestStayTagMethod(GameObject go)
	{
		Debug.Log("Stay tag: " + go.name);
	}

	void TestExitTagMethod(GameObject go)
	{
		Debug.Log("Exit tag: " + go.name);
	}
}