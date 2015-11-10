using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemChecker : MonoBehaviour
{
    public GameObject eventSystem;


     // Use this for initialization
     IEnumerator Start () {
         yield return new WaitForEndOfFrame();

         if (!FindObjectOfType<EventSystem>())
         {
            Instantiate(eventSystem);
         }
     }

	// // Use this for initialization
	// void Awake ()
	// {
	//     if(!FindObjectOfType<EventSystem>())
 //        {
 //           //Instantiate(eventSystem);
 //            GameObject obj = new GameObject("EventSystem");
 //            obj.AddComponent<EventSystem>();
 //            obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
 //            obj.AddComponent<TouchInputModule>();
 //        }
 //	}
}
