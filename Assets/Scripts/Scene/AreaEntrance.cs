using System.Collections;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    [SerializeField] private PlayerDataSave saveLoad;
    [SerializeField] private float delayBeforeActivation = 1f;

    private bool canActivate = false;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            StartCoroutine(ActivateAfterDelay());
            saveLoad.LoadData();
            PlayerController.Instance.transform.position = this.transform.position;
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeActivation);
        canActivate = true;
    }
}
