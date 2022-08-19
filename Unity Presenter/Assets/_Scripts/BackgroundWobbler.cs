using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWobbler : MonoBehaviour
{
    [SerializeField]
    public Transform ObjectToWobble;

    private Quaternion InitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        InitialRotation = ObjectToWobble.rotation;  
    }

    // Update is called once per frame
    void Update()
    {
        var position = ObjectToWobble.position;
        var rotation = ObjectToWobble.rotation;
        var amplitude = .05f;
        var frequency = 0.2f;
        position.x = Mathf.Sin(frequency * Time.time) * amplitude;
        position.y = Mathf.Cos(frequency * Time.time) * amplitude;
        rotation.z = InitialRotation.z + Mathf.Sin(1f * Time.time) * 0.005f;

        ObjectToWobble.position = position;
        ObjectToWobble.rotation= rotation;
    }
}
