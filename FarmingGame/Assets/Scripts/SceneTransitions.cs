using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions instance;
    public RectTransform fader;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(FadeInCo());
    }

    public IEnumerator FadeInCo()
    {
        yield return new WaitForSeconds(0.05f);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.7f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public IEnumerator OpenSceneCo()
    {
        yield return new WaitForSeconds(.5f);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.7f).setOnComplete(() =>
        {
            if (sceneToLoad != "")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Scene to load is empty on");
                return;
            }
        });
    }

    public void OpenScene()
    {
        StartCoroutine(OpenSceneCo());
    }

    public IEnumerator OpenSceneCo_WithParam(string nextScene)
    {
        yield return new WaitForSeconds(.5f);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.7f).setOnComplete(() =>
        {
            if (nextScene != "")
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.Log("Scene to load is empty on");
                return;
            }
        });
    }

    public void OpenScene_WithParam(string nextScene)
    {
        StartCoroutine(OpenSceneCo_WithParam(nextScene));
    }
}
