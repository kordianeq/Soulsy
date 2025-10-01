using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenager : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject CinematicCamera;
    [SerializeField] GameObject GameUi;
    [SerializeField] GameObject CutsceneUi;

    bool QuitCutscene = false;
    // Start is called before the first frame update
    void Start()
    {
        QuitCutscene = false;
        Camera.SetActive(false);
        CinematicCamera.SetActive(true);
        GameUi.SetActive(false);
        CutsceneUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && QuitCutscene == false )
        {
            
            Camera.SetActive(true);
            CinematicCamera.SetActive(false);
            GameUi.SetActive(true);
            CutsceneUi.SetActive(false);
            QuitCutscene = true;
        }
    }
}
