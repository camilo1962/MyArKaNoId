using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenManager : MonoBehaviour
{
    private GameObject pressKeyText;
    private float entryAnim = 1.5f; //LogoEntry anim duración tiempo
    private bool allowToPress = false;

    void Start()
    {
        Screen.SetResolution(540, 960, false);
        if (!pressKeyText) pressKeyText = GameObject.Find("PressKeyText");

        pressKeyText.SetActive(false);

        StartCoroutine(StartInteraction());
    }

    private void Update()
    {
        if (allowToPress && Input.anyKey)
            SceneManager.LoadScene("Menu");
    }
    IEnumerator StartInteraction()
    {
        yield return new WaitForSeconds(entryAnim);
        pressKeyText.SetActive(true);
        allowToPress = true;
    }
}
