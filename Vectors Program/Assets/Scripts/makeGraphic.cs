using UnityEngine;

public class makeGraphic : MonoBehaviour {

    //WILL NEED TO BE CHANGED WHEN VECTORS CAN BE DELETED
    //Number of times vectors have been created
    int i = 0;

    //The prefabricated vector object. Contains several 3D graphical components, and controlling invisible components.
    public GameObject vectorModel;
    private Vector3 position = new Vector3(0f,0f,0f);

    //Holds all of the vector object structures
    private VectorClass[] allVectors = new VectorClass[12];
    //Holds all of the vector 3D models, components, gameobjects, and controlling empties
    private GameObject[] allModels = new GameObject[12];
    //Holds the object that controls the graphics of the vector tails
    private GameObject[] allTails = new GameObject[12];


    //Called by the GUI. When the make vector button is pressed, a vector is made. It's model is stored in one array, and its class is stored in another one.
    public void make() {
        /*When working with Euler angles, 
            Rotations are COUNTER-CLOCKWISE
            x = rotation around x-axis --- changes z component.
            y = rotation around y-axis --- changes x AND y components.
            z will not produce a visible change, in this case
        */
        if (i < allModels.Length) {//If there are fewer than 12 vectors

            allVectors[i] = new VectorClass(50, 50, 50);
            //Instantiate the stored model into the scene, and store a copy of it in an array
            allModels[i] = (GameObject)Instantiate(vectorModel, position, Quaternion.Euler(270f, 0f, 0f));

            //Modify the vector scale, along its forward axis, based on the vector magnitude
            allModels[i].transform.localScale = new Vector3(1f,1f, allVectors[i].magnify());

            //Rename a vector once it is created
            allModels[i].name = "Vector [" + i + "]";

            i++;

            position.x++;
        }else {//If there are more than 12 vectors...
            Debug.Log("Don't go too crazy with the vectors!");
        }
    }
}
