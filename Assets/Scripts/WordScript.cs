using UnityEngine;
using TMPro;

public class WordsManager : MonoBehaviour
{
    public string[] wordPool = { "Deadline", "Clock", "Paper", "Idea", "Project", "Stress", "Notebook", "Coffee" };

    public GameObject wordPrefab;
    public RectTransform spawnArea;

    [Header("Settings")]
    public int wordCount = 6; // how many bubbles spawn

    void Start()
    {
        SpawnWords();
    }

    void SpawnWords()
    {
        for (int i = 0; i < wordCount; i++)
        {
            // pick a random word
            string randomWord = wordPool[Random.Range(0, wordPool.Length)];

            // pick a random position in the area
            Vector2 randomPos = new Vector2(
                Random.Range(-spawnArea.rect.width / 2, spawnArea.rect.width / 2),
                Random.Range(-spawnArea.rect.height / 2, spawnArea.rect.height / 2)
            );

            // make the bubble
            GameObject newWord = Instantiate(wordPrefab, spawnArea);
            newWord.GetComponent<RectTransform>().anchoredPosition = randomPos;

            TMP_Text textComp = newWord.GetComponentInChildren<TMP_Text>();
            textComp.text = randomWord;
        }
    }
}
