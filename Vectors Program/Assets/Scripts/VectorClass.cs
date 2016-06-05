using UnityEngine;

public class VectorClass : MonoBehaviour {

    //Array holding the vector components
    private float[] components;
    //True = algebreic. False = geometric.
    private bool type;
    //Holds the vector's magnitude
    private float mag;
    //For 2d
    public VectorClass(int x, int y) {
        type = true;
        components = new float[2];
    }
    //For 3d
    public VectorClass(int x, int y, int z) {
        type = true;
        components = new float[3];
    }
    //For 4d
    public VectorClass(float x, float y, float z, float w) {
        type = true;
        components = new float[4];
    }
    //For geometric
    public VectorClass(float mag, float deg) {
        type = false;
        components = new float[2];
    }

    //Gets the magnitude of the vector
    public float magnify(float[] components) {
        return 0;
    }
    //Returns the unit vector
    public int[] unitify(float[] components, float magnitude) {
        return null;
    }

    //algebreic dot
    public float dot(VectorClass v1, VectorClass v2) {
        return 0;
    }
    //geometric dot
    public float dot(float magA, float magB, float theta) {
        return 0;
    }
    //algebreic cross
    public int[] cross(VectorClass v1, VectorClass v2) {
        return null;
    }
    //geometric cross
    public int[] cross(float magA, float magB, float theta) {
        return null;
    }
}
