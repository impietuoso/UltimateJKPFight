using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    public bool isThisMainMenu;
    public float transitionDuration;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        if (isThisMainMenu)
            GameManager.instance.HideCanvasGroup(this.GetComponent<CanvasGroup>());
    }

    public void LoadScene(int newScene) {
        StartCoroutine(Load(newScene));
    }

    public IEnumerator Load(int newScene) {
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadSceneAsync(newScene);
    }

    public void ReloadScene() {
        SceneManager.LoadSceneAsync(0);
    }
}
