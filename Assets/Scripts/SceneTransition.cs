using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public RectTransform blackStrip;
    public float transitionSpeed = 1.5f;

    private void Start()
    {
        blackStrip.localPosition = new Vector3(-Screen.width, 0, 0);
    }

    [ContextMenu("Akash")]
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        float elapsedTime = 0;
        Vector3 startPosition = new Vector3(-Screen.width, 0, 0);
        Vector3 endPosition = Vector3.zero;

        while (elapsedTime < 1)
        {
            blackStrip.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        blackStrip.localPosition = endPosition;
        SceneManager.LoadScene(sceneName);
    }
}