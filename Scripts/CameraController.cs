using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform vrHeadset;

    void Update()
    {
        // will get the rotation of the VR headset
        Quaternion vrRotation = vrHeadset.rotation;

        // Convert rotation of vr set to unity calculation
        Quaternion unityRotation = new Quaternion(-vrRotation.x, -vrRotation.y, vrRotation.z, vrRotation.w);

        // Set the cam rot to VR headset rot
        transform.rotation = unityRotation;
    }
}
