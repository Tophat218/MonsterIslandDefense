using UnityEngine;
using System.Collections;
 
public class CameraMovement : MonoBehaviour {
    
    public float mainSpeed = 100.0f; //regular speed
    public float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    public float maxShift = 1000.0f; //Maximum speed when holdin gshift
    public float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); 
    private float totalRun= 1.0f;
     
    void Update () {
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;
       
       
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ 
         
          p = p * Time.deltaTime;
          Vector3 newPosition = transform.position;
          //If player wants to move on X and Z axis only
          if (Input.GetKey(KeyCode.Space)){ 
              transform.Translate(p);
              newPosition.x = transform.position.x;
              newPosition.z = transform.position.z;
              transform.position = newPosition;
          } else {
              transform.Translate(p);
          }
        }
    }
     
    private Vector3 GetBaseInput() { 
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 3);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -3);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-3, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(3, 0, 0);
        }
        return p_Velocity;
    }
}