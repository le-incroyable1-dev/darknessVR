using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class playerController : MonoBehaviour
{

    public GameObject mainCam;
    public GameObject stickFire;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject interactText;
    public Light playerPointLight;
    Vector3 hitPos;
    //public GameObject sample;
    bool looksAtGround = false;
    bool looksAtFire = false;
    bool looksAtCandle = false;
    float speed = 4f;
    float yOffset = 3f;



    // Start is called before the first frame update
    void Start()
    {
        EnterVR();
    }

    // FixedUpdate is preferred for physics-based calculations
    // It runs on regular intervals and may be called more frequently than Update
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Did Hit");

            if(hit.transform.gameObject.CompareTag("Fire") && hit.distance < 10f)
            {
                looksAtGround = false;
                looksAtFire = true;
                looksAtCandle = false;
            }
            else if(hit.transform.gameObject.CompareTag("Ground"))
            {
                looksAtGround = true;
                looksAtFire = false;
                looksAtCandle = false;
                Debug.Log("Hit the Ground!!");
                hitPos = hit.point;

            }
            else if(hit.transform.gameObject.CompareTag("candle") && hit.distance < 10f){
                
                looksAtGround = false;
                looksAtFire = false;
                looksAtCandle = true;
            }
            else{
                looksAtGround = false;
                looksAtFire = false;
                looksAtCandle = false;
            }
        }
        else
        {
            looksAtGround = false;
            looksAtFire = false;
            looksAtCandle = false;
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Check");

        if(Input.touchCount > 0){
          
                if(looksAtGround){
                    float step =  speed * Time.deltaTime;
                    Vector3 targetPoint = new Vector3(hitPos.x, hitPos.y + yOffset, hitPos.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);
                }
                else if(looksAtFire && !stickFire.activeSelf){
                    stickFire.SetActive(true);
                    playerPointLight.enabled = false;
                    text1.SetActive(false);
                    text2.SetActive(true);
                    interactText.SetActive(false);
                }
                else if(looksAtCandle){
                    text2.SetActive(false);
                    text3.SetActive(true);
                }
                
        }
    }

    /// <summary>
    /// Enters VR mode.
    /// </summary>
    private void EnterVR()
    {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }
    /// <summary>
    /// Initializes and starts the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    ///
    /// <returns>
    /// Returns result value of <c>InitializeLoader</c> method from the XR General Settings Manager.
    /// </returns>
    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }
/*
    public void Interact()
    {
        Debug.Log("Interact pls :)");
    }

*/
}
