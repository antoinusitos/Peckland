using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class EditingRoom : MonoBehaviour
{
    [SerializeField]
    private int myCellsX = 10;
    [SerializeField]
    private int myCellsY = 8;

    [SerializeField]
    private int myCellSize = 1;

    [SerializeField]
    private EditingCell myCellPrefab = null;

    [SerializeField]
    private string myCellName = "";

    private EditingCell[] myCells = null;

    [ContextMenu("Init Room")]
    public void GenerateRoom()
    {
        myCells = new EditingCell[myCellsX * myCellsY];

        for (int i = 0; i < myCellsY; i++)
        {
            for (int j = 0; j < myCellsX; j++)
            {
                EditingCell go = Instantiate(myCellPrefab, transform);
                myCells[j + i * myCellsX] = go;
                go.transform.localPosition = new Vector3(j * myCellSize, -i * myCellSize, 0);
            }
        }
    }

    [ContextMenu("Save Room")]
    public void SaveRoom()
    {
        int[] saved = new int[myCells.Length];
        for (int i = 0; i < myCells.Length; i++)
        {
            saved[i] = myCells[i].GetCellType();
        }

        string toWrite = "";
        for (int i = 0; i < saved.Length; i++)
        {
            if(i != 0 && i % myCellsX == 0)
            {
                toWrite += "\n";
            }
            toWrite += saved[i];
        }

        Debug.Log(toWrite);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/Rooms/" + myCellName + ".json");
        sw.Write(toWrite);
        sw.Flush();
        sw.Close();
    }

    [ContextMenu("Clear Room")]
    public void ClearRoom()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i));
        }

        if (myCells == null)
            return;

        for (int i = 0; i < myCells.Length; i++)
        {
            DestroyImmediate(myCells[i].gameObject);
        }
        myCells = null;
    }
}
