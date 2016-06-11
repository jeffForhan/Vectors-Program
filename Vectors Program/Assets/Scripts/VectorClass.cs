using UnityEngine;

//Split into an inheritence tree for generic vectors that branch into 3D, 2D, and 4D
public class VectorClass {

    //The model, colliders etc. that the vector representation holds
    public GameObject vectorObj;

    //Position on a plane
    private Transform pos2D;
    //Position in space
    private Transform pos3D;

    //Array holding the vector components
    private float[] components;
    //Holds the vector's magnitude
    private float magni;

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
        magni = magnify();

    }

    //For 3d #2
    /// <summary>
    /// Constructor:
    /// Pre: User has pressed the add vector button in the GUI
    /// Post: Makes a 3 - dimensional displacement vector, based on x, y, and z components, as well as a displacement from the origin
    /// </summary>
    /// <param name="x">X component</param>
    /// <param name="y">Y component</param>
    /// <param name="z">Z component</param>
    /// <param name="disp">The displacement from the origin</param>
    public VectorClass(int x, int y, int z, Transform disp) {

        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        //Find the magnitude of the vector. Store this value.
        magni = magnify();

    }

    //Gets the vector components
    public float[] getVectComp() {
        return components;
    }

    //Resets the vector components
    public void setVectComp(float x, float y, float z) {
        components[0] = x;
        components[1] = y;
        components[2] = z;
    }

    public float getMagni() {
        return magni;
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
    ////geometric dot (DONE)
    //public float dot(float magA, float magB, float theta) {
    //    float dotVal = 0;

    //    dotVal = magA * magB * Mathf.Cos(Mathf.Deg2Rad * theta);

    //    return dotVal;
    //}

    //algebreic cross (DONE IN JAVA --- TRANSLATE)
    public int[] cross(VectorClass v1, VectorClass v2) {
        return null;
    }
    ////geometric cross
    //public int[] cross(float magA, float magB, float theta) {
    //    return null;
    //}

    public VectorClass projection(VectorClass vA, VectorClass vB) {

        float scalarMulti = vA.dot(vB) / vB.dot(vB);
        
        VectorClass projAonB = vB;
        projAonB.components[0] *= scalarMulti;
        projAonB.components[1] *= scalarMulti;
        projAonB.components[2] *= scalarMulti;

        projAonB.magni = projAonB.magnify();

        return projAonB;
    }
}
