using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    // List of puzzle scene names (make sure they match EXACTLY in Build Settings!)
    private string[] puzzles = { "WordWeb", "ShapeFit", "ConceptMatch" };

    // Called when puzzle is finished early
    public void PuzzleFinished()
    {
        SceneTransition.FadeToScene("DeskScene");
    }

    // Called when time runs out
    public void LoadNextPuzzle()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int index = System.Array.IndexOf(puzzles, currentScene);

        if (index >= 0 && index < puzzles.Length - 1)
        {
            // Go to the next puzzle in the list
            SceneTransition.FadeToScene(puzzles[index + 1]);
        }
        else
        {
            // If no more puzzles left, go to Results
            SceneTransition.FadeToScene("Results");
        }
    }
}