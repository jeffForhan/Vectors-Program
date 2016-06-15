using UnityEngine;
using System.IO;

public class UIController : MonoBehaviour {

    private string pathStart = "Assets\\Saves\\";
    private string fileName = "Vectors_First_Test.HYDE";
    private string path;

    private StreamWriter writer;
    private StreamReader reader;

    void Awake() {

        path = pathStart + fileName;

        try {
            if (!File.Exists(path)) {
                Debug.Log("Made new vector saving file.");
                File.Create(path);
            }
            else {
                Debug.Log("File already exists.");
            }

            writer = new StreamWriter(path);

        }
        catch (FileNotFoundException e) {
            Debug.Log("Cannot find file referenced.");
            Debug.Log(e.Message);
        }
        catch (IOException e) {
            Debug.Log("IO error");
            Debug.LogError(e.Message);
        }
    }

    //When the save button is pressed, hold vectors in a HYDE file
    public void saveBtnPress() {
        try {
            for (int i = 0; i < VectorClass.totVectors; i++) {
                writer.WriteLine(VectorClass.allVectors[i].getVectComp()[0] + " , " + VectorClass.allVectors[i].getVectComp()[1] +
                " , " + VectorClass.allVectors[i].getVectComp()[2]);
            }
            Debug.Log("Saved Vector Components");
            writer.Close();
        }
        catch (IOException e) {
            Debug.Log("Error saving file.");
            Debug.LogError(e.Message);
        }
    }
}
