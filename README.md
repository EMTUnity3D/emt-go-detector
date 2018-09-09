# EMT GameObject detector

With this script for Unity3D, you will have a "Detector" component to set up events based on trigger collisions

## Example

To do this create an empty game object inside your scene, add it a trigger collider and add the component "Detector" to your object.

Once done, you can create a new object and add it the following script component.

```cs
public GameObject detectorObj;

void Start()
{
    Detector detector = detectorObj.GetComponent<Detector>();

    detector.OnLayerEnter("Default", TestMethod1);
    detector.OnLayerExit("Enemies", TestMethod2);
    detector.OnLayerStay("Items", TestMethod3);

    detector.OnTagEnter("Player", TestMethod1);
    detector.OnTagExit("Player", TestMethod2);
    detector.OnTagStay("Item", TestMethod3);
}

void TestMethod1(GameObject intruder)
{
    ...
}

void TestMethod2(GameObject intruder)
{
    ...
}

void TestMethod3(GameObject intruder)
{
    ...
}
```

This code will:

- execute `TestMethod1`whenever a collider enters the detector's trigger collider, as long as the intruder object is set to the layer "Default".

- execute `TestMethod2`whenever a collider leaves the detector's trigger collider, as long as the intruder object is set to the layer "Enemies".

- execute `TestMethod3`while a collider stays inside the detector's trigger collider, as long as that object is set to the layer "Items".

- execute `TestMethod1`whenever a collider enters the detector's trigger collider, as long as the intruder object has a tag named "Player".

- execute `TestMethod2`whenever a collider leaves the detector's trigger collider, as long as the intruder object has a tag named "Player".

- execute `TestMethod3`while a collider stays inside the detector's trigger collider, as long as that object has a tag named "Item".

One requirement is that all the listener methods that are added, in this case `TestMethod1`, `TestMethod2`, `TestMethod3`, must have the proper signature. That is a method with a return type `void` and only one parameter `GamObject`.

And of course **do not forget to set the detector itself in a proper layer** to detect collisions.

```cs
// Example
void MyMethodListener(GameObject gameObject) { ... }
```

## Questions

If you have any questions or doubts about how to use the component, please leave them in the "Issues" section for this repository.

Send me an [email](emt.unity3d@gmail.com) as well.
