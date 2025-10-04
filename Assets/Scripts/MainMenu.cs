using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour

{
    public void StartGame()
    {
        SceneTransition.FadeToScene("DeskScene");
    }
}
