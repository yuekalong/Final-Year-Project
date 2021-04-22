﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Google.Maps.Coord;
using Google.Maps.Unity;
namespace Google.Maps.Examples.Shared {
  /// <summary>
  /// This class controls the behavior of the instructions panel.
  /// It also adjusts the instructions displayed on screen based on the platform where
  /// the example is running.
  ///
  /// The Instructions component has two states controlled by the help button.
  /// When the button is clicked on, the instruction dialog is displayed,
  /// and The help button is disabled.
  /// When the dialog is closed, we re-activate the help button.
  ///
  /// This approach optimizes the UI real estate for the example.
  ///
  /// </summary>
  public class InstructionsHandler : MonoBehaviour {
    /// <summary>
    /// Information text adjusted depending on deployed platform.
    /// </summary>
    public Text InstructionsText;

    private GameObject gameZone;
    public int map=2;
    /// <summary>
    /// Dialog box controlled by the help button.
    /// </summary>
    public GameObject InstructionsDialog;
    /// <summary>
    /// Reference to Help button.
    /// </summary>
    //public GameObject HelpButton;
    /// <summary>
    /// Glass panel used to block events when dialog is on.
    /// </summary>
    public GameObject GlassPanel;

    /// <summary>
    /// At start, update the instructions text based on the target platform,
    /// and hide the Instructions Dialog.
    /// </summary>
    private void Start() {
      Assert.IsNotNull(InstructionsText, "Instructions Text is not set!");
      Assert.IsNotNull(InstructionsDialog, "Instructions Dialog is not set!");
      //Assert.IsNotNull(HelpButton, "Help button is not set!");
      Assert.IsNotNull(GlassPanel, "GlassPanel is not set!");

      InstructionsText.text =
        "You are not in the game zone!!!\nPlease go back inside or you will lose the game in 30s.";



      ShowHideDialog(false);
    }
    public void Update()
    {
      LatLng latLng;
      LocationFollower script= GetComponent<LocationFollower>();
      latLng=script.currentLocation;
      
      if(map==1 && latLng.Lat>0)
      {
        gameZone= GameObject.Find("Map1");
        gameZone.transform.position = new Vector3 (gameZone.transform.position.x,0.5f,gameZone.transform.position.y);
        if(latLng.Lat>22.424400 && latLng.Lat<22.420000 && latLng.Lng>114.200540 && latLng.Lng<114.207000)
          ShowHideDialog(false);
          
        else 
          ShowHideDialog(true);
      }
      if(map==2 && latLng.Lat>0)
      {
        gameZone= GameObject.Find("Map2");
        gameZone.transform.position = new Vector3 (gameZone.transform.position.x,0.5f,gameZone.transform.position.y);
        if(latLng.Lat<22.420000 && latLng.Lat>22.417350 && latLng.Lng>114.202500 && latLng.Lng<114.209440)
          ShowHideDialog(false);
        else 
          ShowHideDialog(true);
      }
      if(map==3 && latLng.Lat>0)
      {
        gameZone= GameObject.Find("Map3");
        gameZone.transform.position = new Vector3 (gameZone.transform.position.x,0.5f,gameZone.transform.position.y);
      }

    }
    /// <summary>
    /// Event triggered when the help button is clicked on.
    /// </summary>

    /// <summary>
    /// Event triggered by any click/touch on the glass panel.
    /// </summary>
    public void OnClose() {
      ShowHideDialog(false);
    }

    /// <summary>
    /// Helper function to hide or show the dialog panel and its associated elements.
    /// </summary>
    /// <param name="isVisible">Indicates if dialog should be visible or hidden.</param>
    private void ShowHideDialog(bool isVisible) {
      //HelpButton.SetActive(!isVisible);
      InstructionsDialog.SetActive(isVisible);
      GlassPanel.SetActive(isVisible);
    }
  }
}
