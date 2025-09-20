using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startingTime = 60f;
    private float timeLeft;
    private bool isRunning = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject timesUpPanel; // Assign in Inspector

    [Header("Puzzle Management")]
    public PuzzleManager puzzleManager;

    void Start()
    {
        timeLeft = startingTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;
        UpdateTimerText();

        if (timeLeft <= 0f)
        {
            TimeUp();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = "Deadline: " + Mathf.Ceil(timeLeft).ToString() + "s";
    }

    private void TimeUp()
    {
        isRunning = false;

        if (timesUpPanel != null)
        {
            timesUpPanel.SetActive(true); // show message
        }

        // Delay before moving to next puzzle
        Invoke(nameof(GoNext), 2f);
    }

    private void GoNext()
    {
        if (puzzleManager != null)
        {
            puzzleManager.LoadNextPuzzle();
        }
    }

    // Call this when player finishes puzzle early
    public void FinishEarly()
    {
        if (!isRunning) return;

        isRunning = false;
        puzzleManager.PuzzleFinished();
    }
}