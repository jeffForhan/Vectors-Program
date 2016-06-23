using UnityEngine;

    //The abstract vector class that all other vectors inherit from

    abstract class Vector {

    //The model, colliders etc. that the vector representation holds
    public GameObject vectorObj;
    //Position on a plane
    private Transform pos2D;
    private float radMultiplier;

    //Number of vectors in the scene
    public static int numVectors;
    public static Vector[] allVectors = new Vector[12];
    public static GameObject[] allModels = new GameObject[12];

    //Vector components
    private float[] components;
    //Magnitudeof the vector
    private float magnitude;
    //The name of the vector
    private string name;

    //The character that says what type of vector it is
    private char type = 'p';

    /*
        Scale up the diameter of the vectors in the screen based on the camera distance and their magnitudes
        Pre: 0.32 seconds has passed since last call
        Post: Scale vectors based on their magnitudes and the distance of the camera from the origin 
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

    //Empty constructor
    public Vector(){
        numVectors++;

    }

    public Vector(float x, float y, float z) {
        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;



        //Find the magnitude of the vector. Store this value.
        magnitude = setMagnitude();
    }

    public Vector(int x, int y, int z, Transform disp) {

        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        //Find the magnitude of the vector. Store this value.
        magnitude = setMagnitude();

    }

    /**/
    public void setName(Vector vector) {

        name = "vector" + numVectors;

    }

    /**/
    public string getName() {
        return name;
    }

    /**/
    public float[] getVectorComponents() {
        return components;
    }

    /**/
    public void setVectorComponents(float x, float y, float z) {
        components[0] = x;
        components[1] = y;
        components[2] = z;
    }

    /**/
    public float getMagnitude() {
        return magnitude;
    }

    /**/
    public float setMagnitude() {

        float magnitude = 0f;

        for (int i = 0; i < components.Length; i++) {
            magnitude += Mathf.Pow(components[i], 2f);
        }
        magnitude = Mathf.Sqrt(magnitude);

        return magnitude;
    }

    /**/
    public void setMagnitude(float newMag) {
        float ratioMagtoX = getMagnitude() / components[0];
        float ratioMagtoY = getMagnitude() / components[1];
        float ratioMagtoZ = getMagnitude() / components[2];

        // ratio = m/x
        // x = m/ratio

        float componentX = newMag / ratioMagtoX;
        float componentY = newMag / ratioMagtoY;
        float componentZ = newMag / ratioMagtoZ;

        //Set new components
        components[0] = componentX;
        components[1] = componentY;
        components[2] = componentZ;
    }

    //Returns the unit vector(DONE)
    public float[] makeUnitVector(float[] componentsU, float magnitude) {
        float[] unitVector = new float[componentsU.Length];
        for (int i = 0; i < componentsU.Length; i++) {
            unitVector[i] = componentsU[i] * (1 / magnitude);
        }
        return unitVector;
    }

    //algebreic dot (DONE)
    public float dot(Vector other) {
        float dotVal = 0;
        //Multiply respective components of the two vectors input
        for (int i = 0; i < components.Length; i++) {
            dotVal += components[i] * other.components[i];
        }
        return dotVal;
    }

    public float dot(Vector vA, Vector vB) {
        float dotVal = 0;

        //Multiply respective components of the two vectors input
        for (int i = 0; i < components.Length; i++) {
            dotVal += vA.components[i] * vB.components[i];
        }
        return (dotVal);
    }

    /*
        Creates a projection of a vector onto another (like a shadow of one on the other)
        Pre: The user presses the projection button, or a vector's rotation is being defined
        Post: Evaluate the projection formula

        Proj_a_on_b = [(a dot b) / (b dot b)] * 
    */
    public Vector projection(Vector vA, Vector vB) {

        float scalarMulti = vA.dot(vB) / vB.dot(vB);

        Vector projAonB = vB;
        projAonB.components[0] *= scalarMulti;
        projAonB.components[1] *= scalarMulti;
        projAonB.components[2] *= scalarMulti;

        projAonB.magnitude = projAonB.setMagnitude();

        return projAonB;
    }

    //No triple scalar product

}
