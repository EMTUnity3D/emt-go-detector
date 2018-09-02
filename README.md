
# Layer-based trigger detection

This script allows you to create events driven by trigger collisions.

## Example

To do this create an empty game object inside your scene, add it a trigger collider and add the component "Detector" to your object.

Once done, you can create a new object and add it the following script component.

```cs
public GameObject detectorObj;
    
void Start()
{
    Detector detector = detectorObj.GetComponent<Detector>();
    
    detector.OnTriggerEnter("Default", TestMethod1);
    detector.OnTriggerExit("Enemies", TestMethod2);
    detector.OnTriggerStay("Items", TestMethod3);
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

* execute `TestMethod1`whenever a collider enters the detector's trigger collider, as long as the intruder object is set to the layer "Default".

* execute `TestMethod2`whenever a collider leaves the detector's trigger collider, as long as the intruder object is set to the layer "Enemies".

* execute `TestMethod3`while a collider stays inside the detector's trigger collider, as long as that object is set to the layer "Items".

One requirement is that all the listener methods that are added, in this case `TestMethod1`, `TestMethod2`, `TestMethod3`,  must have the proper signature. That is a method with a return type `void` and only one parameter `GamObject`.

```cs
// Example
void MyMethodListener(GameObject gameObject) { ... }
```
    
## Questions

If you have any questions or doubts about how to use the component, please leave them in the "Issues" section for this repository.

Send me an [email](enriquemorenotent@gmail.com) as well.
