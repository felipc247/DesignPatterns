using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneHandler : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        LevelManager.Instance.ChangeScene(sceneName);
    }
}
