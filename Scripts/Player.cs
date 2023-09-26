using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    FURNITURE,
    TRANSLATE,
    ROTATE,
    SCALE

}



public class Player : MonoBehaviour
{
    public static Player instance;

    public InputMode activeMode = InputMode.NONE;

    public Object activeFurniturePrefab;
    [SerializeField]
    private float playerSpeed = 3.0f;
    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        instance = this;
    }

    public void TryWalk()
    {
        if(Input.GetMouseButton(0) && activeMode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;

            Vector3 newPosition = transform.position + forward * Time.deltaTime * playerSpeed;

            transform.position = newPosition;
        }
    }
}
