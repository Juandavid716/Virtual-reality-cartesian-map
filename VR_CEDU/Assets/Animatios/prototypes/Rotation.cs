using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    // Assign in the inspector
    public GameObject objectToRotate;
    public Slider z;
    public Slider y;
    public Slider x;

    // Preserve the original and current orientation
    private float previousValue;
    private float previousValue1;
    private float previousValue2;
    void Awake()
    {
        // Assign a callback for when this slider changes
        this.z.onValueChanged.AddListener(this.OnSliderChanged);
        this.y.onValueChanged.AddListener(this.OnSliderChanged1);
        this.x.onValueChanged.AddListener(this.OnSliderChanged2);
        // And current value
        this.previousValue = this.z.value;
        this.previousValue1 = this.y.value;
        this.previousValue2 = this.x.value;
    }

    void OnSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue;
        this.objectToRotate.transform.Rotate(Vector3.forward * delta * 360);

        // Set our previous value for the next change
        this.previousValue = value;
    }

    void OnSliderChanged1(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue1;
        this.objectToRotate.transform.Rotate(Vector3.right * delta * 360);

        // Set our previous value for the next change
        this.previousValue1 = value;
    }

    void OnSliderChanged2(float value)
    {
        // How much we've changed
        float delta = value - this.previousValue2;
        this.objectToRotate.transform.Rotate(Vector3.left * delta * 360);

        // Set our previous value for the next change
        this.previousValue2 = value;
    }
}