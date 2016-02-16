using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Instructions : MonoBehaviour
{
    public GameObject backButton;
    public void Start()
    {
        backButton = Instantiate(backButton, transform.position, Quaternion.identity) as GameObject;
        backButton.transform.SetParent(transform);
        backButton.transform.localPosition = new Vector3(155f, -185f, 0f);
        backButton.transform.localScale = Vector3.one;
        backButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { back(); });
    }

    public void back()
    {
        SceneManager.LoadScene("Scene_Title");
    }
}
