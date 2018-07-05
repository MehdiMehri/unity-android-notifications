using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NotificationTest : MonoBehaviour
{
	public Toggle sound;
	public Toggle light;
	public Toggle vibration;
	public Text status;
	public Text delayText;
	public InputField title;
	public InputField description;
	public InputField action1;
	public InputField action2;
	public Slider delay;
	public Dropdown type;
	public Dropdown icon;
	void Awake()
    {
        LocalNotification.ClearNotifications();
		status.text = "";
    }

	#region UI events ======================================
	public void notificationSend(){

		string title = this.title.text;
		string text = this.description.text;
		string action1 = this.action1.text;
		string action2 = this.action2.text;
		long delay =	Convert.ToInt64(this.delay.value.ToString ()) * 1000;
		string type = this.type.captionText.text;

		if (title == "")
			title = "test title";

		if (text == "")
			text = "test text";

		if (action1 == "")
			action1 = "action 1";

		if (action2 == "")
			action2 = "action 2";
		
		switch (type){

		case "Small Icon Notification":
			status.text = "small icon notification set";
			LocalNotification.SendNotification(1, delay, title, text, new Color32(0xff, 0x44, 0x44, 255), sound.isOn, light.isOn, vibration.isOn, "app_icon", icon.captionText.text);
			break;

		case "Big Icon Notification":
			status.text = "Big icon notification set";
			LocalNotification.SendNotification(1, delay, title, text, new Color32(0xff, 0x44, 0x44, 255), sound.isOn, light.isOn, vibration.isOn, "app_icon", icon.captionText.text);
			break;

		case "Action Notification":
			status.text = "Action notification set";
			LocalNotification.Action actionBtn1 = new LocalNotification.Action ("action1", action1, this);
			actionBtn1.Foreground = false;
			LocalNotification.Action actionBtn2 = new LocalNotification.Action ("action2", action2, this);
			actionBtn2.Foreground = true;
			LocalNotification.SendNotification(1, delay, title, text, new Color32(0xff, 0x44, 0x44, 255), sound.isOn, light.isOn, vibration.isOn, null, icon.captionText.text, "boing", "default", actionBtn1, actionBtn2);
			break;

		case "Repeat":
			status.text = "notification repeat";
			LocalNotification.SendRepeatingNotification(1, delay, 5000, title, text, new Color32(0xff, 0x44, 0x44, 255), sound.isOn, light.isOn, vibration.isOn, null, icon.captionText.text);
			break;

		case "Stop":
			status.text = "notification stop";
			LocalNotification.CancelNotification(1);
			break;

		default:
				
			break;

		}

	}

	public void OnDelayChange(){
		delayText.text = delay.value.ToString ();
	}

	public void exitGame(){
		Application.Quit ();
	}

	public void onTypeChange(){
		//string type = this.type.captionText.text;
		if (type.value != 2){	//type == "Action Notification") {
			action1.interactable = false;
			action2.interactable = false;
		} else {
			action1.interactable = true;
			action2.interactable = true;
		}
	}

	#endregion
}
