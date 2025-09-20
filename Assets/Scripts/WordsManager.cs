using UnityEngine;
using System.Collections.Generic;

public class WordManager : MonoBehaviour
{
    // Singleton instance
    public static WordManager Instance { get; private set; }

    // Dictionary: category -> list of words
    public Dictionary<string, List<string>> categoryDict { get; private set; }
    
    // Full list of words
    public List<Word> allWords { get; private set; }

    [SerializeField] private TextAsset jsonFile; // Assign in inspector or leave empty to load automatically

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadWords();
    }

    private void LoadWords()
    {
        if (jsonFile == null)
            jsonFile = Resources.Load<TextAsset>("Data/AssociatedWords"); // path relative to Resources folder

        WordList wordList = JsonUtility.FromJson<WordList>(jsonFile.text);

        allWords = wordList.words;
        categoryDict = new Dictionary<string, List<string>>();

        foreach (Word w in allWords)
        {
            foreach (string category in w.categories)
            {
                if (!categoryDict.ContainsKey(category))
                    categoryDict[category] = new List<string>();

                categoryDict[category].Add(w.word);
            }
        }

        Debug.Log($"Loaded {allWords.Count} words into {categoryDict.Count} categories.");
    }
}