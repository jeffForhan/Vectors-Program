﻿using UnityEngine;

public class VectorClass : MonoBehaviour {

    public GameObject vectorObj;

    //Position on a plane
    private Transform pos2D;
    //Position in space
    private Transform pos3D;

    //Array holding the vector components
    private float[] components;
    //True = algebreic. False = geometric.
    private bool type;
    //Holds the vector's magnitude
    private float mag;

    //Constructors
    //For 2d
    public VectorClass(int x, int y) {
        type = true;

        //Add vector components to an array
        components = new float[2];
        components[0] = x;
        components[1] = y;

        mag = magnify(components);

    }
    //For 3d
    public VectorClass(int x, int y, int z) {
        type = true;

        //Add vector components to an array
        components = new float[3];
        components[0] = x;
        components[1] = y;
        components[2] = z;

        mag = magnify(components);

    }
    //For 4d
    public VectorClass(float x, float y, float z, float w) {
        type = true;

        //Add vector components to an array
        components = new float[4];
        components[0] = x;
        components[1] = y;
        components[2] = z;
        components[3] = w;

        mag = magnify(components);

    }
    //For geometric
    public VectorClass(float mag, float deg) {
        type = false;
        components = new float[2];

        //Store the magnitude as the first array element and the direction in the second
        components[0] = mag;
        components[1] = deg;
    }

    //Gets the magnitude of the vector(DONE)
    public float magnify(float[] components) {

        float magnitude = 0f;

        for(int i = 0; i < components.Length; i++) {
            magnitude += Mathf.Pow(components[i], 2f);
        }
        magnitude = Mathf.Sqrt(magnitude);

        return magnitude;
    }
    //Returns the unit vector
    public int[] unitify(float[] components, float magnitude) {
        return null;
    }

    //algebreic dot (DONE)
    public float dot(VectorClass v1, VectorClass v2) {
        float dotVal = 0;

        //Multiply respective components of the two vectors input
        for(int i = 0; i < v1.components.Length; i++) {
            dotVal += v1.components[i] * v2.components[i];
        }
        return dotVal;
    }
    //geometric dot (DONE)
    public float dot(float magA, float magB, float theta) {
        float dotVal = 0;

        dotVal = magA * magB * Mathf.Cos(Mathf.Deg2Rad * theta);

        return dotVal;
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
