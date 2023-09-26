using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayObject : MonoBehaviour
{
    public bool isTransformable = false;

    private int objectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    private Vector3 initialObjectRotation;
    private Vector3 initialPlayerRotation;

    private Vector3 initialObjectScale;

    public virtual void OnGazeEnter(RaycastHit hitInfo)
    {
        Debug.Log("Gaze entered on" + gameObject.name);
    }
    public virtual void OnGaze(RaycastHit hitInfo)
    {
        Debug.Log("Gaze hold on" + gameObject.name);
    }
    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze exited on" + gameObject.name);
    }

    public virtual void OnPress(RaycastHit hitInfo)
    {
        Debug.Log("Button pressed");
        if(isTransformable)
        {
            objectLayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            initialObjectRotation = transform.rotation.eulerAngles;
            initialPlayerRotation = Camera.main.transform.eulerAngles;

            initialObjectScale = transform.localScale;
        }
    }
    public virtual void OnHold(RaycastHit hitInfo)
    {
        Debug.Log("Button hold");
        if(isTransformable)
        {
            ObjectTransform(hitInfo);
        }
    }
    public virtual void OnRelease(RaycastHit hitInfo)
    {
        Debug.Log("Button release");

        if(isTransformable)
        {
            gameObject.layer = objectLayer;
        }
    }
    public virtual void ObjectTransform(RaycastHit hitInfo)
    {
        //Call the correct transformation function
        switch(Player.instance.activeMode)
        {
            // call different cases for active mdoe 
            case InputMode.TRANSLATE:
                ObjectTranslate(hitInfo);
                break;
            case InputMode.ROTATE:
                ObjectRotate(hitInfo);
                break;
            case InputMode.SCALE:
                ObjectScale(hitInfo);
                break;
        }
    }

    public virtual void ObjectTranslate(RaycastHit hitInfo)
    {
        //Move the object (position)
        if(hitInfo.collider != null && hitInfo.collider.GetComponent<Floor>())
        {
            transform.position = hitInfo.point;
        }
    }

    public virtual void ObjectRotate(RaycastHit hitInfo)
    {
        //rotate the object(rotation)
        float rotationSpeed = 5.0f;
        Vector3 currentPLayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 currentObjectRotation = transform.rotation.eulerAngles;
        // calculate rotation from currentplayerrot - startplayerrotation 
        Vector3 rotationDelta = currentPLayerRotation - initialPlayerRotation;

        Vector3 newRotation = new Vector3(currentObjectRotation.x, initialObjectRotation.y + (rotationDelta.y * rotationSpeed), currentObjectRotation.z);
        transform.rotation = Quaternion.Euler(newRotation);
    }
    public virtual void ObjectScale(RaycastHit hitInfo)
    {
        //scale the object bigger/smaller (scale)
        float scaleSpeed = 0.1f;

        float scaleFactor = 1;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        //if looking up
        if(rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f)
        {
            //if greater than 180, map it between 0 - 180
            if(rotationDelta.x > 180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }

            scaleFactor = 1.0f + Mathf.Abs(rotationDelta.x) * scaleSpeed;

        } 
        else
        {
            if(rotationDelta.x < -180.0f)
            {
                rotationDelta.x = 360.0f + rotationDelta.x;
            }

            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationDelta.x) * (1.0f/scaleSpeed)) / 180.0f);
        }
        transform.localScale = scaleFactor * initialObjectScale;
    }
}
