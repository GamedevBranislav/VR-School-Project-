using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    public void ResetPos()
    {
        var rotationAngleY = playerHead.transform.rotation.eulerAngles.y -
            resetTransform.rotation.eulerAngles.y;
        player.transform.Rotate(0, -rotationAngleY, 0);

        var distanceDiff = resetTransform.position - playerHead.transform.position;

        player.transform.position += distanceDiff;
    }
}
