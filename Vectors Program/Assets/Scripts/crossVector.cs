using UnityEngine;
using System.Collections;

class CrossProduct : Vector {

        float[] compA = new float[3];
        float[] compB = new float[3];
        string nameA, nameB;

        //calculates cross product
        public CrossProduct(Vector vectorA, Vector vectorB) {

            //Get vector parent names
            nameA = vectorA.getName();
            nameB = vectorB.getName();

            //Get components of the parent vectors
            compA = vectorA.getVectorComponents();
            compB = vectorB.getVectorComponents();

            //Set the vector components using the cross product formula
            setVectorComponents(compA[1] * compB[2] - compB[1] * compA[2] , compA[2] * compB[0] - compB[2] * compA[0] , compA[0] * compB[1] - compB[0] * compA[1]);

        }

        //compares names of vectors from cross product
        public void changeVector(Vector vectorChange) {
            if (vectorChange.getName().Equals(nameA)) {
                //Changes the relevant component
                compA = vectorChange.getVectorComponents();

                //Recreates the cross product components
                setVectorComponents(compA[1] * compB[2] - compB[1] * compA[2], compA[2] * compB[0] - compB[2] * compA[0], compA[0] * compB[1] - compB[0] * compA[1]);

                //Delete the old model
                //Create new model

        }
            else if (vectorChange.getName().Equals(nameB)) {
                //Changes the relevant component
                compB = vectorChange.getVectorComponents();

                //Recreates the cross product componenets
                setVectorComponents(compA[1] * compB[2] - compB[1] * compA[2], compA[2] * compB[0] - compB[2] * compA[0], compA[0] * compB[1] - compB[0] * compA[1]);

                //Delete the old model
                //Create new model
        }
        else {
                //Nothing
            }

        }
}
