using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScene : MonoBehaviour {

 public AudioClip sfxButton;
 
//  private bool oneshotSfx;
 
 // Update is called once per frame
 void Update () {
  
  //if press any key jump to gameplay scene
  if(Input.anyKeyDown)
  {
//    if(!oneshotSfx)
//    {
    // AudioSource.PlayClipAtPoint(sfxButton,Vector3.zero);
    Invoke("LoadScene",0.5f);
    // oneshotSfx = true;
//    }
   
   
  }
 
 }
 
 void LoadScene()
 {
  //load gameplay scene
  Application.LoadLevel("Sandbox");
 }
 
}
