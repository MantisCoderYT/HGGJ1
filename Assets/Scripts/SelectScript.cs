using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    public string SceneToLoad;

    public void LoadProject()
    {
        SceneTransition.FadeToScene(SceneToLoad);
    }
  
}
