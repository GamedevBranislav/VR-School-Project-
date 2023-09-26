using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    [SerializeField] GameObject reticle;

    public Color inactiveReticleColor = Color.gray;

    public Color activeReticleColor = Color.green;

    private RayObject currentRayObject;

    private RayObject currentSelectedObject;
    private RaycastHit lastHit;

    
    void Start()
    {
        SetReticleColor(inactiveReticleColor);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastLook();
        CheckForInput(lastHit);
    }

    public void RaycastLook()
    {
        Ray raycastRay = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 20);

        if(Physics.Raycast(raycastRay, out hitInfo))
        {
            //get the gameobject from hit
            GameObject hitObj = hitInfo.collider.gameObject;
            //get rayobject from the hit object
            RayObject rayObj = hitObj.GetComponent<RayObject>();

            //object has a rayobject component
            if(rayObj != null)
            {
                //object were looking at is different
                if(rayObj != currentRayObject)
                {
                    ClearCurrentObject();
                    currentRayObject = rayObj;
                    currentRayObject.OnGazeEnter(hitInfo);
                    SetReticleColor(activeReticleColor);
                }
                else
                {
                    currentRayObject.OnGaze(hitInfo);
                }
            }
            else
            {
                ClearCurrentObject();
            }

            lastHit = hitInfo;
        }
        else
        {
            ClearCurrentObject();
        }
    }

    private void SetReticleColor(Color reticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void CheckForInput(RaycastHit hitInfo)
    {
        //check for mouse down (click)
        if(Input.GetMouseButtonDown(0) && currentRayObject != null)
        {
            currentSelectedObject = currentRayObject;
            currentSelectedObject.OnPress(hitInfo);
        }
        //check for mouse hold (holding mouse button)
        else if (Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnHold(hitInfo);
        }
        //check for release (releasing mouse button)
        else if (Input.GetMouseButtonUp(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnRelease(hitInfo);
            currentSelectedObject = null;
        }
    }

    private void ClearCurrentObject()
    {
        if(currentRayObject != null)
        {
            currentRayObject.OnGazeExit();
            SetReticleColor(inactiveReticleColor);
            currentRayObject = null;
        }
    }
}
