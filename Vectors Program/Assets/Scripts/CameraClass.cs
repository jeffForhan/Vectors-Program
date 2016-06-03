using UnityEngine;
public class CameraClass : MonoBehaviour {


    /*
    TO DO
    Make the low bound based on the XY component rather than XYZ
    */

    //The anchor is an object that the camera is parented to. Transformations of this object affect the transformations of the camera

    //The position, scale, and rotation of the anchor
    private Transform anchor;
    //The position, scale, and rotation of the camera
    private Transform cam;

    //Used for inputs
    //
    private float xInfluence;
    //
    private float yInfluence;
    //
    private float scrollInfluence;

    //A scalar that controls how fast the camera moves.
    private float speed = 150f;
    //How much the radius is changing.
    private Vector3 deltaRadius;

    //Variables to limit how small radius can be
    private Vector3 disp;
    private float dispMag;
    private float lowBound = 3f;

    //Getters and Setters (unimplemented)
    //public float getRadius() {
    //    return deltaRadius;
    //}
    //public void setRadius(float rad) {
    //    deltaRadius = rad;
    //}

    //Other methods
    private void turn() {

        //Positive if scrolling forward. Negative if scrolling backward. As scrolling speed increases, the influence moves farther from 0.
        scrollInfluence = Input.GetAxis("Mouse ScrollWheel");
        
        if (Input.GetMouseButton(0)) {//Is the left mouse button being held
            //Negative if mouse is going left. Positive if mouse is going right. Mouse speed acts as a constant multiple.
            xInfluence = Input.GetAxis("Mouse X");

            if (xInfluence != 0f) {
                //Rotates the anchor along is upwards axis by the frame-drawing time, the speed constant, and the the mouse movement.
                anchor.transform.Rotate(Vector3.up * speed * xInfluence * Time.deltaTime);
            }

        } else if (Input.GetMouseButton(1)) {//If right mouse button is held
            
            //Negative if the mouse is going down, positive if up. Mouse speed acts as a constant multiple.
            yInfluence = Input.GetAxis("Mouse Y");

            //Moves the camera along the upwards axis, based on the mouse input, the frame-drawing time, and half of the speed (to make it easier to handle) constant.
            cam.position = new Vector3(cam.position.x, cam.position.y + yInfluence * Time.deltaTime * -speed/2, cam.position.z);

        } else if(scrollInfluence != 0f) {//If the scrollwheel is being used

            //Get the displacement of the camera from the anchor
            disp = new Vector3(cam.position.x - anchor.position.x, cam.position.y - anchor.position.y, cam.position.z - anchor.position.z);
            
            //Get the magnitude of the displacement from the anchor, in the horizontal plane
            dispMag = Mathf.Sqrt(Mathf.Pow(disp.x, 2f) + Mathf.Pow(disp.z, 2f));

            if (dispMag > lowBound || scrollInfluence < 0f) {//If the user is scrolling out or is not too close, change the camera position

                //Multiply the scrolling input by the frame-drawing time, and the speed comstant
                deltaRadius = scrollInfluence * Time.deltaTime * speed * 3f * anchor.forward;

                //Add the vector based on the zooming to the camera's position vector
                cam.position = cam.position + deltaRadius;
            }
        }
        //Make the camera's forward axis face the anchor
        cam.LookAt(anchor);
    }


    //UNITY DEFINED METHODS
        //Awake() is called when the script is loading
        //Update() is called each time a frame is redrawn

    private void Awake() {//Initialize values
           
        //Find the transformation, scale, and rotation of the camera, and the anchoring objects

        //Find object tagged as anchor
        anchor = GameObject.FindGameObjectWithTag("ANCHOR").transform;
        //Find object tagged as camera
        cam = GameObject.FindGameObjectWithTag("CAMERA").transform;
        
        //Set up initial camera viewing angle
        cam.transform.LookAt(anchor);
    }
    private void Update() {//Call every frame, to update the scene
        turn();
    }
}
