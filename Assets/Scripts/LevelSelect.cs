using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public UnityEngine.UI.Button curButton;
    public GameObject levelButton, s1, s2, s3, s4, backButton;
    const int LENGTH_1 = 8;
    const int LENGTH_2 = 3;
    const int LENGTH_3 = 6;
    const int LENGTH_4 = 6;
    int series;
    Vector3 firstPos = new Vector3(-55, 100, 0);
    public void buttonClicked(int seriesNum)
    {
        int length = 0;
        series = seriesNum;
        switch(seriesNum)
        {
            case 1:
                length = LENGTH_1;
                break;
            case 2:
                length = LENGTH_2;
                break;
            case 3:
                length = LENGTH_3;
                break;
            case 4:
                length = LENGTH_4;
                break;
        }
        s1.SetActive(false);
        s2.SetActive(false);
        s3.SetActive(false);
        s4.SetActive(false);
        for(int i = 1; i <= length; ++i)
        {
            GameObject newButton = Instantiate(levelButton, transform.position, Quaternion.identity) as GameObject;
            newButton.name = i.ToString();
            newButton.transform.SetParent(transform);
            newButton.transform.localScale = Vector3.one;
            curButton = newButton.GetComponent<UnityEngine.UI.Button>();
            newButton.transform.FindChild("Text").GetComponent<Text>().text = "Level " + i.ToString();
            curButton.onClick.AddListener(delegate { select(newButton.name); });
            if (i % 2 == 0)
            {
                ColorBlock colors = curButton.colors;
                colors.normalColor = Vector4.Normalize(new Vector4(152f, 255f, 255f, 157f));
                curButton.colors = colors;
                newButton.transform.localPosition = firstPos - (i - 1) * (Vector3.up * 35f) - (Vector3.left * 130f) ;
            }
            else
            {
                newButton.transform.localPosition = firstPos - i * (Vector3.up * 35f);
            }
        }
        backButton = Instantiate(backButton, transform.position, Quaternion.identity) as GameObject;
        backButton.transform.SetParent(transform);
        backButton.transform.localPosition = new Vector3(155f, -185f, 0f);
        backButton.transform.localScale = Vector3.one;
        backButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { back(); });
    }
    public void select(string name)
    {
        SceneManager.LoadScene(string.Format("Level{0}-{1}", series, name));
    }
    public void back()
    {
        SceneManager.LoadScene("Scene_Title");
    }
}
