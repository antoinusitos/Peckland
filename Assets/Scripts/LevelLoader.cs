using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader myInstance = null;

    public static LevelLoader GetInstance()
    {
        return myInstance;
    }

    private Dictionary<string, List<List<int>>> myLoadedLevelCells = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        myInstance = this;
        LoadLevelCells();
        SceneManager.LoadScene(1);
    }

    private void LoadLevelCells()
    {
        myLoadedLevelCells = new Dictionary<string, List<List<int>>>();
        string[] files = Directory.GetFiles(Application.dataPath + "/Rooms/");
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Contains(".meta"))
                continue;

            StreamReader streamReader = new StreamReader(files[i]);
            string content = streamReader.ReadToEnd();

            List<int> level = new List<int>();
            for (int c = 0; c < content.Length; c++)
            {
                string s = content[c].ToString();
                if (s != "\n")
                {
                    level.Add(int.Parse(s));
                }
            }

            string[] path = files[i].Split('.');
            string[] names = path[0].Split('/');
            string nameWithNumber = names[names.Length - 1];
            string[] name = nameWithNumber.Split('_');
            if (!myLoadedLevelCells.ContainsKey(name[0]))
            {
                myLoadedLevelCells.Add(name[0], new List<List<int>>() { level });
            }
            else
            {
                myLoadedLevelCells[name[0]].Add(level);
            }
        }
    }

    public List<List<int>> GetLevels(string aName)
    {
        if(myLoadedLevelCells.ContainsKey(aName))
        {
            return myLoadedLevelCells[aName];
        }
        return null;
    }
}
