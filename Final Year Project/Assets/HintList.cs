using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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
  public class HintList : MonoBehaviour {
    /// <summary>
    /// Information text adjusted depending on deployed platform.
    /// </summary>
    public Text InstructionsText;

    /// <summary>
    /// Dialog box controlled by the help button.
    /// </summary>
    public GameObject InstructionsDialog;
    /// <summary>
    /// Reference to ListButton button.
    /// </summary>
    public GameObject CloseButton;
    /// <summary>
    /// Glass panel used to block events when dialog is on.
    /// </summary>
    public GameObject GlassPanel;

    public int haveHint =0;

    public string hint_words="";



    private void Start() {
      Assert.IsNotNull(InstructionsText, "Instructions Text is not set!");
      Assert.IsNotNull(InstructionsDialog, "Instructions Dialog is not set!");
      Assert.IsNotNull(CloseButton, "Close button is not set!");
      Assert.IsNotNull(GlassPanel, "GlassPanel is not set!");

      InstructionsText.text = "You Have No Hint!!!\nGo And Find It Before Your Enermy Take It!!!";

      hint_words=PlayerPrefs.GetString("hint_stored","Empty");
      if(hint_words!="Empty")
      {
          InstructionsText.text=hint_words;
      }
      ShowHideDialog(false);
    }
    public void Update()
    {
      if(hint_words!="Empty")
      {
          InstructionsText.text=PlayerPrefs.GetString("hint_stored");
      }
    }

    public void HideHint() {
      ShowHideDialog(false);
    }

    public void ShowHint() {
        ShowHideDialog(true);
    }



    private void ShowHideDialog(bool isVisible) {
      CloseButton.SetActive(isVisible);
      InstructionsDialog.SetActive(isVisible);
      GlassPanel.SetActive(isVisible);
    }
  }

