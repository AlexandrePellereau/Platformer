using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    private int _level;
    private const int MaxLevel = 3;

    public string filename;
    public List<char> letters;
    public List<GameObject> prefabs;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        _level = 1;
        
        if (letters.Count != prefabs.Count)
        {
            Debug.LogError("Letters and prefabs lists must have the same size");
        }
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }
            
            sr.Close();
        }
        
        int row = 0;

        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] line = currentLine.ToCharArray();
            for (int column = 0; column < line.Length; column++)
            {
                char letter = line[column];
                if (!letters.Contains(letter))
                    continue;
                
                GameObject prefab = prefabs[letters.IndexOf(letter)];
                Vector3 scale = prefab.transform.localScale;
                Vector3 newPos = new Vector3(column + scale.x / 2, row + scale.y / 2, 0);
                Instantiate(prefab, newPos, Quaternion.identity, environmentRoot);
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }

    public void NextLevel()
    {
        if (_level == MaxLevel)
        {
            _level = 1;
        }
        else
        {
            _level++;
        }
        filename = $"level_{_level}";
        ReloadLevel();
    }
}
