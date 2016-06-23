﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class UIController : MonoBehaviour {

    #region GENERAL_VARIABLES

    //UI



    //Text used for user messages
    public Text dialogueMessage;
    //Gets file name 
    public InputField fileNameIn;
    //Gets the name of the first vector being operated on
    public InputField operationInA;
    //Gets the name of the second vector being operated on
    public InputField operationInB;

    //IO
    //Writes to a file
    private StreamWriter writer;
    //reads file
    private StreamReader reader;

    //Primitives
    //Array holding the strings from the .HYDE file
    private string[] storedVectors = new string[12];

    //The path to the folder that holds all save files
    private string pathStart = "Saves\\";

    //The file name (Could be set up by the user)
    private string fileName = "default.HYDE";
    //Holds the full path
    private string fullPath;

    private float counter;

    #endregion GENERAL_VARIABLES

    #region GENERAL_INTERACTION

    //OPERATION METHODS

    /*
        Takes strings input into text fields, and searches  through the names of the vectors present
        Pre: An operation is performed on two vectors by the user
        Post: All vectors are compared to the strings input
    */
    public static int[] findVectors(string nameA, string nameB) {

        int count = 0;

        int numA = -1;
        int numB = -1;

        //Array holding the indeces of the vectors found for the user
        int[] vectorPositions = { numA, numB };

        for (int i = 0; i < VectorClass.totVectors; i++) {//Loop for all vectors that are present in the scene

            if (nameA.Equals(nameB, StringComparison.InvariantCultureIgnoreCase) && nameA.Equals(VectorClass.allModels[i].name, StringComparison.InvariantCultureIgnoreCase)) {//if the names are the same
                numA = i;
                numB = i;
                count += 2;
            }
            else if (nameA.Equals(VectorClass.allModels[i].name, StringComparison.InvariantCultureIgnoreCase)) {//Check the first user input versus the current vectorname. Ignore casing.
                numA = i;
                count++;
            }
            else if (nameB.Equals(VectorClass.allModels[i].name, StringComparison.InvariantCultureIgnoreCase)) {//Check the second user input versus the current vector name. Ignore casing.
                numB = i;
                count++;
            }
        }

        if (count == 2) {//If two names were found
            vectorPositions[0] = numA;
            vectorPositions[1] = numB;
        }
        //If there were unfound names, return {-1,-1}. If not return the positions found.
        return vectorPositions;
    }

    /*
        Prints the dot product of two vectors to the screen
        Pre: User clicks the DOT PRODUCT button
        Post: The two vectors being operated on are found, dotted, and the result is printed
    */
    public void printDot() {

        //Holds array indeces of the vectors being operated on
        int[] vectorPositions = findVectors(operationInA.text, operationInB.text);

        if (vectorPositions[0] != -1) {//if the first index returned is not -1, then calculated

            //Get the two vectors
            VectorClass first = VectorClass.allVectors[vectorPositions[0]];
            VectorClass second = VectorClass.allVectors[vectorPositions[1]];

            //The result of the dot product
            float dotReturn = first.dot(second);

            //Print to the screen
            dialogue("Dot Product : " + dotReturn.ToString(), 6f);
        }
        else {//Tell the user that one or more vectors can't be found
            dialogue("Could not find one or more of vectors input.", 6f);
        }
    }

    //IO METHODS

    /*
        Modifies the file being accessed by this script
        Pre: User finishes editing the file name input field
        Post: The file name of the path being accessed is changed
    */
    public void chooseFileName() {
        //Change the current file name
        fileName = fileNameIn.text + ".HYDE";
        //Change the current path
        fullPath = pathStart + fileName;
    }

    /*
        Makes a save file, and adds the vectors in this scene to it
        Pre: User presses the save button from the UI
        Post: A writer object stores the current vectors in a .HYDE file
    */
    public void saveBtnPress() {
        if (fileNameIn.text != string.Empty) {//Checks if the user has input a file name
            try {
                //Links the writer to the file
                writer = new StreamWriter(fullPath);
                //Sends all vectors to the .HYDE file. Stores the components in format "x,y,z"
                for (int i = 0; i < VectorClass.totVectors; i++) {
                    writer.WriteLine(VectorClass.allVectors[i].getType() + " ( " + VectorClass.allVectors[i].getComponents()[0] + " , " + VectorClass.allVectors[i].getComponents()[1] +
                    " , " + VectorClass.allVectors[i].getComponents()[2] + " ) ");
                }
                dialogue("Saved vector components");

                //Closes the writing stream
                writer.Close();
            }
            catch (NullReferenceException e) {//If the file doesn't exist, display an error message
                dialogue("Error : File has not yet been created, or dies not exist. Error: " + e.Message, 8f);
            }
            catch (IOException e) {//If there is a general IO error, tell the user
                dialogue("Error : Difficulty saving file: " + e.Message, 8f);
            }
        }
        else {//Tell the user if there is no file to write to
            dialogue("No file name specified");
        }
    }

    /*
        Generates a saved set of vectors
        Pre: The user presses the load button with a file specified
        Post: That file is read, and the info held is stored in an array
    */
    public void loadBtnPress() {
        if (File.Exists(fullPath)) {
            //The current string being stored
            string currentMessage;

            //Loop controlling variable #1 (for reading)
            int rep = 0;
            //Loop controlling variable #2 (for printing)
            int rep2 = 0;

            try {
                //Connect the file reader to the file being read from
                reader = new StreamReader(fullPath);

                //While there is text in a line, store it to the current string, and add it to the string array
                while ((currentMessage = reader.ReadLine()) != null) {
                    storedVectors[rep] = currentMessage;
                    rep++;
                }

                //Close the reader once it's done
                reader.Close();
            }
            catch (IOException e) {
                dialogue("Error : Difficulty reading file : " + e.Message, 8f);
            }

            //A string holding all of the vectors that have been created
            string list = "";
            //Print out
            while (storedVectors[rep2] != null) {//Add all of the stored vectors to a string ****error when at max capacity
                if (rep2 == 0) {
                    //Add to a string that holds the created vectors. Used to start the list.
                    list += "Created Vectors : " + storedVectors[rep2];
                }
                else {
                    //Used to add to the list.
                    list += " , " + storedVectors[rep2];
                }

                rep2++;
            }
            //Print out the list to the screen
            dialogue(list, 6f);
        }
        else {//Tell the user if there is not file specified, or the file does not exist.
            dialogue("No file to read from.");
        }
    }

    #endregion GENERAL_INTERACTION

    #region VECTOR_VARAIABLES

    //VECTOR CREATION

    //WILL NEED TO BE CHANGED WHEN VECTORS CAN BE DELETED

    //The prefabricated vector object. Contains several 3D graphical components, and controlling invisible components.
    public GameObject vectorModel;

    //UI input fields for making a new vector
    public InputField _xIn;
    public InputField _yIn;
    public InputField _zIn;

    public InputField xDispIn;
    public InputField yDispIn;
    public InputField zDispIn;

    //Values input for vector
    private float xComp;
    private float yComp;
    private float zComp;

    //Values input for displacement
    private float xDComp;
    private float yDComp;
    private float zDComp;

    private char typeToMake = 'p';

    //Displacement of the vector
    private Vector3 displacement = new Vector3(0f, 0f, 0f);

    #endregion VECTOR_VARIABLES

    #region VECTOR_CREATION

    /*
        Make a vector from scratch
        Pre: User presses the make vector button
        Post: Component input fields are parsed, their values are stored, and the make vector method is called
    */
    public void parseComponents() {

        //Get the input vector components from the text fields
        try {
            xComp = float.Parse(_xIn.text);
            yComp = float.Parse(_yIn.text);
            zComp = float.Parse(_zIn.text);

            makeNew();
        }
        catch (FormatException) {
            dialogue("Invalid Components : Position. All components must be elements of the real numbers.", 8f);
        }

    }

    /*
        Make a vector off the origin
        Pre: User presses the make vector button after having filled out all displacement fields
        Post: Makes a vector that has been displaced from the origin
    */
    public void parseDisplacement() {
        if(xDispIn.text != string.Empty && yDispIn.text != string.Empty && zDispIn.text != string.Empty) {
            try {
                //Get displacement components
                displacement.x = float.Parse(xDispIn.text);
                displacement.y = float.Parse(yDispIn.text);
                displacement.z = float.Parse(zDispIn.text);
            }
            catch(FormatException) {
                dialogue("Invalid Componenets : Displacement. All components must be elements of the real numbers.", 8f);
            }
        }
        else {
            Debug.Log("Set displacement to 0");
            //Make the displacement the 0 vector
            displacement = Vector3.zero;
        }
        //Clear displacement text
        xDispIn.text = string.Empty;
        yDispIn.text = string.Empty;
        zDispIn.text = string.Empty;
    }

    /*
        If possible, makes a cross product vector
        Pre: User fills the name fields, and presses the cross button
        Post: A new vector is made using the components recwived from the cross product
    */
    public void examineCrossFields() {//WORKS BUT DOES NOT LIKE 2D VECTORS, OR WHEN INPUTS ARE THE SAME

        int[] vectorPosInList = findVectors(operationInA.text, operationInB.text);
        float[] crossComp = new float[2];

        if (vectorPosInList[0] != -1) {//Check if the search went through

            VectorClass first = VectorClass.allVectors[vectorPosInList[0]];
            VectorClass second = VectorClass.allVectors[vectorPosInList[1]];

            crossComp = first.crossProduct(first.getComponents()[0], first.getComponents()[1], first.getComponents()[2], second.getComponents()[0], second.getComponents()[1], second.getComponents()[2]);

            xComp = crossComp[0];
            yComp = crossComp[1];
            zComp = crossComp[2];

            //Set the components of the next vector to be made
            typeToMake = 'c';

            //Make a vector using the cross product components
            makeNew();
        }
        else {
            dialogue("One or more incorrect vector names. Cannot cross.", 6f);
        }
    }

    //Called by the GUI. When the make vector button is pressed, a vector is made. It's model is stored in one array, and its class is stored in another one.
    public void makeNew() {

        /*
            When working with Euler angles, 
            Rotations are CLOCKWISE
            x = rotation around x-axis --- changes z component.
            y = rotation around y-axis --- changes x AND y components.
            z will not produce a visible change, in this case
        */

        //check the displacement fields
        parseDisplacement();

        if (VectorClass.totVectors < VectorClass.allModels.Length) {//If there are fewer than 12 vectors
            if (xComp == 0 && yComp == 0 && zComp == 0) {
                dialogue("Cannot represent the zero vector");
            }
            else {
                //Make a vector object to represent the vector being made
                VectorClass.allVectors[VectorClass.totVectors] = new VectorClass(xComp, yComp, zComp, typeToMake);

                //Instantiate the stored model into the scene, and store a copy of it in an array
                VectorClass.allModels[VectorClass.totVectors] = (GameObject)Instantiate(vectorModel, displacement, Quaternion.Euler(findEuler(VectorClass.allVectors[VectorClass.totVectors])));

                //Rename a vector once it is created
                VectorClass.allModels[VectorClass.totVectors].name = "V" + (VectorClass.totVectors);

                //Modify the vector scale, along its forward axis, based on the vector magnitude
                VectorClass.allModels[VectorClass.totVectors].transform.localScale = new Vector3(1f, 1f, VectorClass.allVectors[VectorClass.totVectors].magnify());

                dialogue("Made new vector : " + VectorClass.allModels[VectorClass.totVectors], 10f);

                //ERROR WITH LINE WHEN AT MAX CAPACITY
                VectorClass.totVectors++;
            }
        }
        else {//If there are more than 12 vectors...
            dialogue("Scene is full");

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
        VectorClass vectXY = new VectorClass(vector.getComponents()[0], vector.getComponents()[1], 0f);
        VectorClass iHat = new VectorClass(1f, 0f, 0f);
        VectorClass projXYonIHat = vector.projection(vectXY, iHat);

        //Variables to adjust the rotation so that it represents how vectors should look in a cartesian space
        int hAngleMod = 0;
        int vAnglMod = 1;
        int fwdOrBwd = 1;

        //Create offsets so that the Euler angles used to rotate vectors model vectors in a cartesian space
        if (xComp < 0 && yComp >= 0) {
            hAngleMod = 270;
        }
        else if (xComp <= 0 && yComp < 0) {
            hAngleMod = 270;
            fwdOrBwd = -1;
        }
        else if (xComp > 0 && yComp < 0) {
            hAngleMod = 90;
        }
        else if (xComp >= 0 && yComp >= 0) {
            hAngleMod = 90;
            fwdOrBwd = -1;
        }

        //if the z Input is negative, make the vector go downward
        if (zComp < 0) {
            vAnglMod = -1;
        }
        else {
            vAnglMod = 1;
        }

        //The angle of rotation from the starting point
        float angleXY;

        //Find the angle along the horizontal plane
        if (vectXY.getMagnitude() != 0f) {//If the vector has X or Y components
            angleXY = hAngleMod + fwdOrBwd * Mathf.Rad2Deg * Mathf.Acos(projXYonIHat.getMagnitude() / vectXY.getMagnitude());
        }
        else {//If the vector has no X or Y components, don't alter the angle
            angleXY = 0;
        }
        //Get the angle up from the vertical
        float angleZ = vAnglMod * Mathf.Rad2Deg * Mathf.Acos(vectXY.getMagnitude() / vector.getMagnitude());

        //Account for euler angle direction or rotation
        Vector3 eulerRotComp = new Vector3(-angleZ, angleXY, 0f);

        return eulerRotComp;
    }

    #endregion VECTOR_CREATION

    #region OPERATIONS
    #endregion OPERATIONS

    #region UNITY_DEFINED_METHODS
    //UNITY METHODS

    void callScaleWithDistance() {
        VectorClass.scaleWithDistance();
    }

    void Awake() {//Used for initialization
                  
        //Change the diameter of vectors based on the distance between the origin and the camera (update every 0.32 seconds)
        InvokeRepeating("callScaleWithDistance", 0f, 0.32f);

        dialogue("Hello World!");

        if (!Directory.Exists(pathStart)) {
            dialogue("Created save directory.");
            Directory.CreateDirectory(pathStart);
        }

        //The full path
        fullPath = pathStart + fileName;
    }

    void Update() {//Called at each frame
        if (dialogueMessage.IsActive()) {//Used for removing messages from the screen
            counter += Time.deltaTime;
            if (counter > messageDuration) {
                dialogueMessage.enabled = false;
            }
        }
    }
    #endregion UNITY_DEFINED_METHODS

    float messageDuration = 3f;

    /*
        Sends a dialogue message to notify the user of something
        Pre: The user interacts with something
        Post: A message is displayed in a text element
    */
    public void dialogue(string message) {
        messageDuration = 3f;
        dialogueMessage.text = message;
        dialogueMessage.enabled = true;
        counter = 0f;
    }

    /*
        Sends a message to the user
        Pre: The user interacts with the user
        Post: A message is displayed for the user. It lasts as duration
    */
    public void dialogue(string message, float duration) {
        messageDuration = duration;
        dialogueMessage.text = message;
        dialogueMessage.enabled = true;
        counter = 0f;
    }

    /*
        Closes the program
    */
    public void quit() {
        Debug.Log("Exited application.");
        Application.Quit();
    }
}