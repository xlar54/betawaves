using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour {

    public GameObject arrow;

    void Update()
    {
        float h = Input.GetAxis("Vertical");
        if (h > 0)
        {
            Vector3 v = arrow.transform.localPosition;
            v.y = -53.3f;
            arrow.transform.localPosition = v;
        }

        if (h < 0)
        {
            Vector3 v = arrow.transform.localPosition;
            v.y = -115.8f;
            arrow.transform.localPosition = v;
        }

        if (Input.GetButton("Fire1"))
        {
            Vector3 v = arrow.transform.localPosition;

            if (v.y == -53.3f)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                Application.Quit();
            }
        }
    }

}
