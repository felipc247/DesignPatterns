using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DesignPatterns.Generics;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] GameObject background;
    [SerializeField] Image loadingImage;

    //#region SINGLETON
    //private static LevelManager _instance;
    //private static readonly object _lock = new object();
    //public static LevelManager Instance
    //{
    //    get 
    //    {
    //        lock (_lock) // lucchetto per assicurarci che ci sia un solo thread o coroutine che prova a chiamare questo Get
    //        {
    //            if (_instance == null) // in questo caso è la prima volta che viene chiamato il get di Instance
    //            {
    //                _instance = FindFirstObjectByType<LevelManager>();

    //                if (_instance == null)
    //                {
    //                    GameObject levelManager = Instantiate(Resources.Load<GameObject>("LevelManager"), Vector3.zero, Quaternion.identity);
    //                }
    //            }

    //            return _instance;
    //        }
    //    }
    //}
    //#endregion

    //private void Awake()
    //{
    //    lock (_lock)
    //    {
    //        if (_instance != null && _instance != this)
    //        {
    //            Destroy(gameObject);
    //            return;
    //        }
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    public override void Awake()
    {
        base.Awake();

        // other stuff
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingImage.fillAmount = 0;
        background.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += Operation_completed;

        while (!operation.isDone)
        {
            loadingImage.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }

    private void Operation_completed(AsyncOperation obj)
    {
        background.SetActive(false);
    }
}
