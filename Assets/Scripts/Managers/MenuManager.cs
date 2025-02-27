using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject clickSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void NewRun()
    {
        Instantiate(clickSFX);
        Debug.Log("StartNewRun");
        StartCoroutine(LoadScene("Arena"));
    }

    public void Quit()
    {
        Instantiate(clickSFX);
        StartCoroutine(QuitRoutine());
    }

    private IEnumerator QuitRoutine()
    {

        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public static IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
