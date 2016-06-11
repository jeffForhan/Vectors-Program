using System;
using UnityEngine;
using UnityEngine.UI;

public class makeGraphic : MonoBehaviour {

    //WILL NEED TO BE CHANGED WHEN VECTORS CAN BE DELETED
    //Number of times vectors have been created
    int i = 0;

    //The prefabricated vector object. Contains several 3D graphical components, and controlling invisible components.
    public GameObject vectorModel;

    //UI input fields;
    public InputField _xIn;
    public InputField _yIn;
    public InputField _zIn;

    //Values input
    private float xComp;
    private float yComp;
    private float zComp;

    //Displacement of the vector
    private Vector3 position = new Vector3(0f,0f,0f);

    //Holds all of the vector object structures
    private VectorClass[] allVectors = new VectorClass[12];
    //Holds all of the vector 3D models, components, gameobjects, and controlling empties
    private GameObject[] allModels = new GameObject[12];

    //Called by the GUI. When the make vector button is pressed, a vector is made. It's model is stored in one array, and its class is stored in another one.
    public void make() {
        /*When working with Euler angles, 
            Rotations are CLOCKWISE
            x = rotation around x-axis --- changes z component.
            y = rotation around y-axis --- changes x AND y components.
            z will not produce a visible change, in this case
        */

        //Get the input vector components from the text fields
        try {
            xComp = float.Parse(_xIn.text);
            yComp = float.Parse(_yIn.text);
            zComp = float.Parse(_zIn.text);
        }catch(FormatException) {
            Debug.Log("Invalid Components. All components must be elements of the real numbers.");
        }

        if (i < allModels.Length) {//If there are fewer than 12 vectors
            if (xComp == 0 && yComp == 0 && zComp == 0) {
                Debug.Log("Cannot represent the zero vector");
            }
            else {
                //Make a vector object to represent the vector being made
                allVectors[i] = new VectorClass(xComp, yComp, zComp);

                //Instantiate the stored model into the scene, and store a copy of it in an array
                allModels[i] = (GameObject)Instantiate(vectorModel, position, Quaternion.Euler(findEuler(allVectors[i])));

                //Rename a vector once it is created
                allModels[i].name = "Vector [" + i + "]";

                //Modify the vector scale, along its forward axis, based on the vector magnitude
                allModels[i].transform.localScale = new Vector3(1f, 1f, allVectors[i].magnify());

                i++;
            }
        }else {//If there are more than 12 vectors...
            Debug.Log("Don't go too crazy with the vectors!");
        }
    }

    /// <summary>
    /// Gets the rotation of the vector gameobject
    /// Pre: User has input components, and pressed create vector
    /// Post: The vector made is rotated using projections and adjustments
    /// </summary>
    /// <param name="vector">The vector that has been created by the user</param>
    /// <returns></returns>
    private Vector3 findEuler(VectorClass vector) {
        VectorClass vectXY = new VectorClass(vector.getVectComp()[0], vector.getVectComp()[1], 0f);
        VectorClass iHat = new VectorClass(1f, 0f, 0f);
        VectorClass projXYonIHat = vector.projection(vectXY, iHat);

        //Variables to adjust the rotation so that it represents how vectors should look in a cartesian space
        int hAngleMod = 0;
        int vAnglMod = 1;
        int fwdOrBwd = 1;

        //The vector that has been projected onto the x axis
        Debug.Log(projXYonIHat.getVectComp()[0] + ", " + projXYonIHat.getVectComp()[1] + ", " + projXYonIHat.getVectComp()[2]);

        //Create offsets so that the Euler angles used to rotate vectors model vectors in a cartesian space
        if(xComp < 0 && yComp >= 0) {
            hAngleMod = 270;
        }
        else if(xComp <= 0 && yComp < 0) {
            hAngleMod = 270;
            fwdOrBwd = -1;
        }
        else if(xComp > 0 && yComp < 0) 
        {
            hAngleMod = 90;
        }
        else if(xComp >= 0 && yComp >= 0) {
            hAngleMod = 90;
            fwdOrBwd = -1;
        }

        //if the z Input is negative, make the vector go downward
        if(zComp < 0) {
            vAnglMod = -1;
        }else {
            vAnglMod = 1;
        }

        Debug.Log( projXYonIHat.getMagni()+ " , " +vectXY.getMagni());
        //Find the angle along the horizontal plane
        float angleXY = hAngleMod + fwdOrBwd * Mathf.Rad2Deg * Mathf.Acos(projXYonIHat.getMagni() / vectXY.getMagni());
        //Get the angle up from the vertical
        float angleZ = vAnglMod * Mathf.Rad2Deg * Mathf.Acos( vectXY.getMagni() / vector.getMagni());
        
        Debug.Log(angleXY + ", " + angleZ);
        
        //Account for euler angle direction or rotation
        Vector3 eulerRotComp = new Vector3(-angleZ, angleXY, 0f);

        return eulerRotComp;
    }
}
