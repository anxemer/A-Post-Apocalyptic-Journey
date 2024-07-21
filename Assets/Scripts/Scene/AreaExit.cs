using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private PlayerDataSave saveLoad;
    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransitioning && other.gameObject.GetComponent<PlayerController>())
        {
            isTransitioning = true;
            saveLoad.SaveData();
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            StartCoroutine(TransitionToScene());
        }
    }

    private IEnumerator TransitionToScene()
    {
        yield return new WaitForSeconds(0.5f); // Delay to ensure save/load completes
        SceneManager.LoadScene(sceneToLoad);
    }
}
