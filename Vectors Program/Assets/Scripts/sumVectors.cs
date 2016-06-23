using UnityEngine;
using System.Collections;

public class sumVectors : MonoBehaviour {

    public sumVectors() {
        
    }

    //compares names of vectors from sum
    public void changeVector(Vector vectorChange) {
        if (vectorChange.getName().Equals(nameA)) {
            //Changes the relevant component
            compA = vectorChange.getVectorComponents();

            //Recreates the vector
            getVectorComponents()[0] = compA[0] + compB[0];
            getVectorComponents()[1] = compA[1] + compB[1];
            getVectorComponents()[2] = compA[2] + compB[2];


        }
        else if (vectorChange.getName().Equals(nameB)) {
            //Changes the relevant component
            compB = vectorChange.getVectorComponents();

            //Recreates the vector
            getVectorComponents()[0] = compA[0] + compB[0];
            getVectorComponents()[1] = compA[1] + compB[1];
            getVectorComponents()[2] = compA[2] + compB[2];

        }
        else {
            //Nothing

        }
    }

}
