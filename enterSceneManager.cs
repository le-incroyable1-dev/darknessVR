using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Google.XR.Cardboard;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class enterSceneManager : MonoBehaviour
{

    private Camera _mainCamera;
    // Field of view value to be used when the scene is not in VR mode. In case
    // XR isn't initialized on startup, this value could be taken from the main
    // camera and stored.
    private const float _defaultFieldOfView = 60.0f;


    private void Awake(){
        _mainCamera = Camera.main;

    }

    private void Start(){
        ExitVR();
    }

    public void LoadVRScene(){
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Exits VR mode.
    /// </summary>
    private void ExitVR()
    {
        StopXR();
    }

    /// <summary>
    /// Stops and deinitializes the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");

        _mainCamera.ResetAspect();
        _mainCamera.fieldOfView = _defaultFieldOfView;
    }
}
