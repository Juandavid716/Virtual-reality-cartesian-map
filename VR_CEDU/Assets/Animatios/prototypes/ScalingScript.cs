using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScalingScript : MonoBehaviour {
    public Slider z;
    public Slider y;
    public Slider x;

    private Vector3 originalScale;
    // Use this for initialization
    private void Start () {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update () {
        transform.localScale = originalScale + new Vector3(x.value, y.value, z.value);
    }
}
