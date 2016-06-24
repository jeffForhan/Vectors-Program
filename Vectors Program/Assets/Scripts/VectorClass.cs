using UnityEngine;

//**Split into an inheritence tree for generic vectors that branch into 3D, 2D, and 4D
public class VectorClass {

    //Number of times vectors have been created
    public static int numVectors = 0;
    //Holds all of the vector object structures
    public static VectorClass[] allVectors = new VectorClass[12];
    //Holds all of the vector 3D models, components, gameobjects, and controlling empties
    public static GameObject[] allModels = new GameObject[12];

    //The model, colliders etc. that the vector representation holds
    public GameObject vectorObj;
    //Used to change radial distance of vectors
    private float radMultiplier = 1f;

    //Position in space
    private Vector3 pos3D;

    //Array holding the vector components
    private float[] components;
    //Holds the vector's magnitude
    private float magnitude;

    private char type = 'p';

    private string name;

    private VectorClass[] parents = new VectorClass[2];

    //For 3d
    /// <summary>
    /// Constructor:
    /// Pre: User has pressed the add vector button in the GUI
    /// Post: Makes a 3 - dimensional position vector, based on x, y, and z components
    /// </summary>
    /// <param name="x">X component</param>
    /// <param name="y">Y component</param>
    /// <param name="z">Z component</param>
    public VectorClass(float x, float y, float z) {

        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        //Find the magnitude of the vector. Store this value.
        magnitude = magnify();
    }

    /*
        Constructor:
        Used for the creation of new vectors. Specifying that is is either a cross or it is not
        Pre: A vector is added by the user
        Post: A new vector object is created
    */
    public VectorClass(float x, float y, float z, char _type) {

        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        //Find the magnitude of the vector. Store this value.
        magnitude = magnify();

        //Determines if this should be noted as a position, displacement, cross, or sum vector
        type = _type;
    }

    public VectorClass(int x, int y, int z, VectorClass parentA, VectorClass parentB, bool _type) {
        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        //Find the magnitude of the vector. Store this value.
        magnitude = magnify();
    }

    /*
        Pre: components are required for the calculation
        Post: return a float array holding the components
    */
    public float[] getComponents() {
        return components;
    }

    /*
        Resets vector components being held
        Pre: User resets the vector components
        Post: Change the stored components, and changes the magnitude of the vector
    */
    public void setComponents(float x, float y, float z) {
        components[0] = x;
        components[1] = y;
        components[2] = z;
        magnitude = magnify();
    }

    /*
        Returns the magnitude of a vector
        Pre:
        Post:
    */
    public float getMagnitude() {
        return magnitude;
    }

    public char getVectorType() {
        return type;
    }

    public void setPos3D(Vector3 originOffset) {
        pos3D = originOffset;
    }

    public Vector3 getPos3D() {
        return pos3D;
    }

    public void setName(string _name) {
        name = _name;
    }

    public string getName() {
        return name;
    }

    /*
        Sets the parent vectors of a cross or sum vector
    */
    public void setParents (VectorClass p1, VectorClass p2) {
        parents[0] = p1;
        parents[1] = p2;
    }
    
    /*
        Gets the parent vectors of a sum or cross vector
    */
    public VectorClass[] getParents() {
        return parents;
    }

    //Gets the magnitude of the vector(DONE)
    public float magnify() {

        float magnitude = 0f;

        for(int i = 0; i < components.Length; i++) {
            magnitude += Mathf.Pow(components[i], 2f);
        }
        magnitude = Mathf.Sqrt(magnitude);

        return magnitude;
    }
    //Returns the unit vector(DONE)
    public float[] unitify(float[] componentsU, float magnitude) {
        float[] unitVector = new float[componentsU.Length];
        for(int i = 0; i < componentsU.Length; i++) {
            unitVector[i] = componentsU[i] * (1 / magnitude);
        }
        return unitVector;
    }

    //algebreic dot (DONE)
    public float dot( VectorClass other) {
        float dotVal = 0;

        //Multiply respective components of the two vectors input
        for(int i = 0; i < components.Length; i++) {
            dotVal += components[i] * other.components[i];
        }
        return dotVal;
    }

    //Written by Emily and Amelia
    //Translated to C Sharp from Java by Jeffrey
    //Changed return type to float array from string
    //Changed doubles to floats to work with the rest of the program
    //The rest was conserved
    public float[] crossProduct(float x0, float y0, float z0, float x1, float y1, float z1) {
        float x = 0, y = 0, z = 0;

        x = (y0 * z1) - (y1 * z0);
        y = (z0 * x1) - (z1 * x0);
        z = (x0 * y1) - (x1 * y0);

        float[] coordinates = new float[3];

        coordinates[0] = x;
        coordinates[1] = y;
        coordinates[2] = z;

        return (coordinates);


    }
    /*
        Creates a projection of a vector onto another (like a shadow of one on the other)
        Pre: The user presses the projection button, or a vector's rotation is being defined
        Post: Evaluate the projection formula

        Proj_a_on_b = [(a dot b) / (b dot b)] * 
    */
    public VectorClass projection(VectorClass vA, VectorClass vB) {

        float scalarMulti = vA.dot(vB) / vB.dot(vB);
        
        VectorClass projAonB = vB;
        projAonB.components[0] *= scalarMulti;
        projAonB.components[1] *= scalarMulti;
        projAonB.components[2] *= scalarMulti;

        projAonB.magnitude = projAonB.magnify();

        return projAonB;
    }

    /*
    Pre: Frame is updated
    Post: Scale of vector is changed based on view distance
*/
    public static void scaleWithDistance() {

        //When vectors are very far away, make them larger. When they are close make them bigger
        for (int i = 0; i < numVectors; i++) {
            if (CameraClass.radiusMag >= 200f && allVectors[i].getMagnitude() >= 50f) {
                allVectors[i].radMultiplier = 7f;
            }
            else if (CameraClass.radiusMag >= 50f) {
                allVectors[i].radMultiplier = 1.5f;
            }
            else {
                allVectors[i].radMultiplier = 1f;
            }
            allModels[i].transform.localScale = new Vector3(allVectors[i].radMultiplier, allVectors[i].radMultiplier, allVectors[i].getMagnitude());
        }
    }

    //void sort() {
    //    VectorClass value = new VectorClass(0,0,0);
    //    bool swap = true;

    //    while (swap == true) {
    //        swap = false;
    //        for (int i = 0; i < VectorClass.totVectors; i++) {
    //            if (VectorClass.totVectors > 1) {
    //                    if (VectorClass.allVectors[i].getMagni() > VectorClass.allVectors[i + 1].getMagni()) {
    //                        value = VectorClass.allVectors[i];
    //                        VectorClass.allVectors[i] = VectorClass.allVectors[i + 1];
    //                        VectorClass.allVectors[i + 1] = value;
    //                        swap = true;
    //                    }
    //            }
    //        }
    //    }
    //    for (int j = 0; j < VectorClass.totVectors; j++) {
    //        Debug.Log("Magnitude for vector [" + j + "] " + VectorClass.allVectors[j].getMagni());
    //    }
    //}
}
