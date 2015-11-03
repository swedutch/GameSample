using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;


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

		string connectionStr = "URI=" + Application.dataPath + "/Plugins/MTGDB.sqlite";
		IDbConnection dbcon = (IDbConnection)new SqliteConnection (connectionStr);
		dbcon.Open ();

		IDbCommand dbcmd = dbcon.CreateCommand();

		Debug.Log ("db: " + dbcon.Database.ToString());
		Debug.Log ("state: " + dbcon.State.ToString());
		Debug.Log ("cstr: " + dbcon.ConnectionString.ToString());

		string sql = "CREATE TABLE highscoresNilss (name VARCHAR(20), score INT)";
		dbcmd.CommandText = sql;
		dbcmd.ExecuteNonQuery();
		
		sql = "SELECT * FROM Sets";
		Debug.Log ("running: " + sql);
		dbcmd.CommandText = sql;
		
		IDataReader reader = dbcmd.ExecuteReader();

		Debug.Log ("field count: " + reader.FieldCount.ToString());
		Debug.Log ("depth: " + reader.Depth.ToString());

		while (reader.Read()) {
			Debug.Log ("val0: " + reader.GetValue(0).ToString());
			Debug.Log ("Set: " + reader.GetString(1));
		}

			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbcon.Close();
			dbcon = null;
	}
}
