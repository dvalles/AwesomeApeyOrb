using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotate a text field (degrees/s)
 * Turns off when canvas is off
 */

public class RotateElement : MonoBehaviour
{
    public enum axis {up, forward, right};
    public enum direction {clockwise, counter}

    public axis alongAxis = axis.up;
    public direction whichWay = direction.clockwise;
    public float degreesPerS;
    public bool resetRotation = true;

    // Start is called before the first frame update
    IEnumerator resetRoutine;
    void Start()
    {
        resetRoutine = IResetRotation();
        StartCoroutine(resetRoutine); //hotfix (Dom) for weird rotation problem
        // StartCoroutine(ITurnOff())
    }

    // void OnDestroy()
    // {
    //     if (resetRoutine != null)
    //         StopCoroutine(resetRoutine);
    // }

    // Update is called once per frame
    void Update()
    {
        //counter clockwise
        if (whichWay == direction.counter) {
            float degrees = degreesPerS*Time.deltaTime;
            if (alongAxis == axis.up)
                transform.Rotate(Vector3.up, degrees);
            if (alongAxis == axis.forward)
                transform.Rotate(Vector3.forward, degrees);
            if (alongAxis == axis.right)
                transform.Rotate(Vector3.right, degrees);
        }
        
        //clockwise
        if (whichWay == direction.clockwise) {
            float degrees = -degreesPerS*Time.deltaTime;
            if (alongAxis == axis.up)
                transform.Rotate(Vector3.up, degrees);
            if (alongAxis == axis.forward)
                transform.Rotate(Vector3.forward, degrees);
            if (alongAxis == axis.right)
                transform.Rotate(Vector3.right, degrees);
        }
    }

    WaitForSeconds medWait = new WaitForSeconds(10f);
    Canvas m_Canv;
    CanvasGroup m_CanvG;
    IEnumerator IResetRotation()
    {
        m_Canv = transform.parent.GetComponent<Canvas>();
        while(true) {
            if (resetRotation && m_Canv.enabled == false) //make sure its off when reset
                transform.localRotation = Quaternion.identity;
            yield return medWait;
        }
    }

    // WaitForSeconds shortWait = new WaitForSeconds(10f);
    // IEnumerator IResetRotation()
    // {
    //     m_Canv = transform.parent.GetComponent<Canvas>();
    //     while(true) {
    //         if (resetRotation && m_Canv.enabled == false) //make sure its off when reset
    //             transform.localRotation = Quaternion.identity;
    //         yield return shortWait;
    //     }
    // }
}
