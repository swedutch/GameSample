using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Search : MonoBehaviour {

	public void DoSearch()
	{
		InputField InputFieldComponent;
		Dropdown DropDownComponent;
		Toggle ToggleComponent;
		string[,] searchFields = new string[,]
		{
			{"CardNameInput", "inputfield"},
			{"CardTextInput", "inputfield"},
			{"SetDropdown", "dropdown"},
			{"TypeInput", "inputfield"},
			{"SubTypeInput", "inputfield"},
			{"MythicRarityCheckbox", "toggle"},
			{"RareRarityCheckbox", "toggle"},
			{"UncommonRarityCheckbox", "toggle"},
			{"CommonRarityCheckbox", "toggle"},
			{"SpecialRarityCheckbox", "toggle"},
			{"ManaCostComparatorDropDown", "dropdown"},
			{"ManaCostInput", "inputfield"},
			{"PowerComparatorDropDown", "dropdown"},
			{"PowerInput", "inputfield"},
			{"ToughnessComparatorDropDown", "dropdown"},
			{"ToughnessInput", "inputfield"},
			{"WhiteSymbolToggle", "toggle"},
			{"BlueSymbolToggle", "toggle"},
			{"BlackSymbolToggle", "toggle"},
			{"RedSymbolToggle", "toggle"},
			{"GreenSymbolToggle", "toggle"},
			{"ColorlessSymbolToggle", "toggle"},
			{"ColorResultsOptionsDropdown", "dropdown"}
		};

		for (int i = 0; i < searchFields.Length / 2; i++) 
		{
			GameObject GameObj = GameObject.Find (searchFields[i,0]);

			switch(searchFields[i,1])
			{
				case "inputfield":
					InputFieldComponent = GameObj.GetComponent<InputField> ();
					Debug.Log (searchFields[i,0] + ": " + InputFieldComponent.text);
					break;
				case "dropdown":
					DropDownComponent = GameObj.GetComponent<Dropdown> ();
					Debug.Log (searchFields[i,0] + ": " + DropDownComponent.value.ToString() + " - " + DropDownComponent.options[DropDownComponent.value].text);
					break;
				case "toggle":
					ToggleComponent = GameObj.GetComponent<Toggle> ();
					Debug.Log (searchFields[i,0] + ": " + (ToggleComponent.isOn ? "Checked" : "Unchecked"));
					break;
			}
		}
	}
}
