using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Reflection;
using Mono.Data.Sqlite;


public class Search : MonoBehaviour {
	
	public string Name="";
	public string Text="";
	public string FlavorText="";
	public string Type="";
	public string SubType="";
	public string Set="";
	public bool bMythic=true;
	public bool bRare=true;
	public bool bUncommon=true;
	public bool bCommon=true;
	public bool bSpecial=true;
	public string ManaCostComparator="";
	public string ManaCost="";
	public string PowerComparator="";
	public string Power="";
	public string ToughnessComparator="";
	public string Toughness="";
	public bool bWhite=true;
	public bool bBlue=true;
	public bool bBlack=true;
	public bool bRed=true;
	public bool bGreen=true;
	public bool bColorless=true;
	public int ColorOpts= 0;

	public void GetFieldValues()
	{
		InputField InputFieldComponent;
		Dropdown DropDownComponent;
		Toggle ToggleComponent;
		string[,] searchFields = new string[,]
		{
			{"CardNameInput", "inputfield"},
			{"CardTextInput", "inputfield"},
			{"FlavorTextInput", "inputfield"},
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
				
				switch(searchFields[i,0])
				{
				case "CardNameInput":
					Name = InputFieldComponent.text.Replace("'","`");
					break;
				case "CardTextInput":
					Text = InputFieldComponent.text.Replace("'","`");
					break;
				case "FlavorTextInput":
					FlavorText = InputFieldComponent.text.Replace("'","`");
					break;
				case "TypeInput":
					Type = InputFieldComponent.text.Replace("'","`");
					break;
				case "SubTypeInput":
					SubType = InputFieldComponent.text.Replace("'","`");
					break;
				case "ManaCostInput":
					ManaCost = InputFieldComponent.text.Replace("'","`");
					break;
				case "PowerInput":
					Power = InputFieldComponent.text.Replace("'","`");
					break;
				case "ToughnessInput":
					Toughness = InputFieldComponent.text.Replace("'","`");
					break;
				}
				break;
				
			case "dropdown":
				DropDownComponent = GameObj.GetComponent<Dropdown> ();
				Debug.Log (searchFields[i,0] + ": " + DropDownComponent.value.ToString() + " - " + DropDownComponent.options[DropDownComponent.value].text);
				
				switch(searchFields[i,0])
				{
				case "SetDropdown":
					Set = DropDownComponent.options[DropDownComponent.value].text;
					break;
				case "ManaCostComparatorDropDown":
					ManaCostComparator = DropDownComponent.options[DropDownComponent.value].text;
					break;
				case "PowerComparatorDropDown":
					PowerComparator = DropDownComponent.options[DropDownComponent.value].text;
					break;
				case "ToughnessComparatorDropDown":
					ToughnessComparator = DropDownComponent.options[DropDownComponent.value].text;
					break;
				case "ColorResultsOptionsDropdown":
					ColorOpts = DropDownComponent.value;
					break;
				}
				break;
				
			case "toggle":
				ToggleComponent = GameObj.GetComponent<Toggle> ();
				Debug.Log (searchFields[i,0] + ": " + (ToggleComponent.isOn ? "Checked" : "Unchecked"));
				
				switch(searchFields[i,0])
				{
				case "MythicRarityCheckbox":
					bMythic = ToggleComponent.isOn;
					break;
				case "RareRarityCheckbox":
					bRare = ToggleComponent.isOn;
					break;
				case "UncommonRarityCheckbox":
					bUncommon = ToggleComponent.isOn;
					break;
				case "CommonRarityCheckbox":
					bCommon = ToggleComponent.isOn;
					break;
				case "SpecialRarityCheckbox":
					bSpecial = ToggleComponent.isOn;
					break;
				case "WhiteSymbolToggle":
					bWhite = ToggleComponent.isOn;
					break;
				case "BlueSymbolToggle":
					bBlue = ToggleComponent.isOn;
					break;
				case "BlackSymbolToggle":
					bBlack = ToggleComponent.isOn;
					break;
				case "RedSymbolToggle":
					bRed = ToggleComponent.isOn;
					break;
				case "GreenSymbolToggle":
					bGreen = ToggleComponent.isOn;
					break;
				case "ColorlessSymbolToggle":
					bColorless = ToggleComponent.isOn;
					break;
				}
				break;
			}
		}
	}

	public void DoSearch()
	{
		GetFieldValues ();
		string query = BuildQuery ();

		string connectionStr = "URI=" + Application.dataPath + "/Plugins/MTGDB.sqlite";
		IDbConnection dbcon = (IDbConnection)new SqliteConnection (connectionStr);
		dbcon.Open ();

		IDbCommand dbcmd = dbcon.CreateCommand();

		dbcmd.CommandText = query;
		dbcmd.ExecuteNonQuery();

		IDataReader reader = dbcmd.ExecuteReader();

		Debug.Log ("db: " + dbcon.Database.ToString());
		Debug.Log ("state: " + dbcon.State.ToString());
		Debug.Log ("cstr: " + dbcon.ConnectionString.ToString());
		Debug.Log ("Query: " + query);
		Debug.Log ("field count: " + reader.FieldCount.ToString());
		Debug.Log ("depth: " + reader.Depth.ToString());
		Debug.Log ("num records: " + reader.RecordsAffected.ToString());

		while (reader.Read()) 
		{
			// grab data here
		}

			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbcon.Close();
			dbcon = null;
	}

	private string BuildQuery()
	{
		string q = "", LogicOp = "";
		
		q = "SELECT ";
		q += "CardInSet.Set_id, Card.Card_id, CardInSet.CardNumber, Card.TypeLine, Card.CardName, Card.CardText, Card.CastingCost, ";
		q += "Card.Power, Card.Toughness, Card.White, Card.Blue, Card.Black, Card.Red, Card.Green, ";
		q += "Card.ConvertedManaCost, Sets.SetName, Rarity.Rarity, CardArt.ImagePath, CardInSet.FlavorText, CardInSet.multiverse_id, CardArt.Artist, Card.isSplit , Card.splitName ";
		q += "FROM `Card`, `CardInSet`, `CardArt`, `Sets`, `Rarity`";

		if (SubType != "" && !(SubType.Contains("&") || SubType.Contains("|")))
		{
			q += ", `SubTypeOfCard`, `SubType`";
		}
		if (Type != "" && !(Type.Contains("&") || Type.Contains("|")))
		{
			q += ", `TypeOfCard`, `Type`";
		}
		
		q += " WHERE ";
		
		q += "CardInSet.Card_id=Card.Card_id AND CardArt.CardArt_id=CardInSet.CardArt_id ";
		q += "AND Sets.Set_id=CardInSet.Set_id ";
		q += "AND Rarity.Rarity_id=CardInSet.Rarity_id ";
		
		if (SubType != "")
		{
			if (SubType.Contains("&") || SubType.Contains("|"))
			{
				q += ParseAndOrsORoutputNormal(SubType, "Card.TypeLine");
			}
			else
			{
				q += ParseAndOrsORoutputNormal(SubType, "SubType.SubType");
				q += " AND SubType.SubType_id=SubTypeOfCard.SubType_id";
				q += " AND SubTypeOfCard.Card_id=Card.Card_id";
			}
		}
		if (Type != "")
		{
			if (Type.Contains("&") || Type.Contains("|"))
			{
				q += ParseAndOrsORoutputNormal(Type, "Card.TypeLine");
			}
			else
			{
				q += ParseAndOrsORoutputNormal(Type, "Type.Type");
				q += " AND Type.Type_id=TypeOfCard.Type_id";
				q += " AND TypeOfCard.Card_id=Card.Card_id";
			}
		}
		
		if (Name != "")
		{
			q += ParseAndOrsORoutputNormal(Name, "Card.CardName");
		}
		
		if (Text != "")
		{
			q += ParseAndOrsORoutputNormal(Text, "Card.CardText");
		}
		
		if (FlavorText != "")
		{
			q += ParseAndOrsORoutputNormal(FlavorText, "CardInSet.FlavorText");   
		}
		
		if (Set != "")
		{
			if(Set.Contains("-"))
			{
				if (Set.Contains("---"))
				{
					Set = Set.Replace("-", "");
					q += " AND Sets.Block LIKE '%" + Set + "%' ";
				}
				else
				{
					Set = Set.Replace("-", "").ToLower();
					int thisyear = StrToInt(DateTime.Now.ToString("yyyy"));
					int endyear = thisyear;

					if (StrToInt(DateTime.Now.ToString("MM")) < 10)
					{
						endyear--;
					}

					int cutoff = 10;

					switch (Set)
					{
						case "standard":
							endyear = endyear - 1;
						break;
						case "extended":
							endyear = endyear - 3;
						break;
						case "modern":
							cutoff = 7;
							endyear = 2003;
						break; 
						case "default":
							//searcher.Abort();
						break;
					}

					q += " AND (Sets.ReleaseDate between #" + cutoff + "/01/" + endyear.ToString() + "# AND #10/01/" + thisyear.ToString() + "#)";
					q += " AND (Sets.Block <> 'Stand Alone Sets' AND Sets.Block <> 'Duel Decks' AND Sets.Block <> 'Premium Deck Series'";
					q += " AND Sets.Block <> 'From the Vault' AND Sets.Block <> 'Starter Sets' AND Sets.Block <> 'Original Sets')";
				}
			}
			else
			{
				q += " AND Sets.SetName LIKE '%" + Set + "%' ";
			}
		}
		
		if (!bMythic || !bRare || !bUncommon || !bCommon || !bSpecial)
		{
			string rarityStr = "";
			q += " AND (Rarity.Rarity = '";
			if (bMythic)
			{
				rarityStr += "Mythic Rare";
			}
			if (bRare)
			{
				if (rarityStr != "") { rarityStr += "' OR Rarity.Rarity = '"; }
				rarityStr += "Rare";
			}
			if (bUncommon)
			{
				if (rarityStr != "") { rarityStr += "' OR Rarity.Rarity = '"; }
				rarityStr += "Uncommon";
			}
			if (bCommon)
			{
				if (rarityStr != "") { rarityStr += "' OR Rarity.Rarity = '"; }
				rarityStr += "Common";
			
				if (rarityStr != "") { rarityStr += "' OR Rarity.Rarity = '"; }
				rarityStr += "Basic Land";
			}
			if (bSpecial)
			{
				if (rarityStr != "") { rarityStr += "' OR Rarity.Rarity = '"; }
				rarityStr += "Special";
			}
			q += rarityStr + "')";
		}
		
		if (Power != "" && (PowerComparator != null && PowerComparator != ""))
		{
			LogicOp = PowerComparator;
			if (LogicOp == "!=")
			{
				LogicOp = "<>";
			}
			q += " AND Card.Power " + LogicOp + " '" + Power + "'";
		}
		
		if (Toughness != "" && (ToughnessComparator != null || ToughnessComparator != ""))
		{
			LogicOp = ToughnessComparator;
			if (LogicOp == "!=")
			{
				LogicOp = "<>";
			}
			q += " AND Card.Toughness " + LogicOp + " '" + Toughness + "'";
		}
		
		if (ManaCost != "" && (ManaCostComparator != null || ManaCostComparator != ""))
		{
			LogicOp = ManaCostComparator;
			if (LogicOp == "!=")
			{
				LogicOp = "<>";
			}
			q += " AND Card.ConvertedManaCost " + LogicOp + " " + ManaCost + "";
		}
		
		q += ColorRestrictions();
		
		
		return q;
	}

	private string ColorRestrictions()
	{
		
		string str = "";
		bool flag = false;
		bool wh = bWhite, rd = bRed, bu = bBlue, bl = bBlack, gr = bGreen;
		bool cls = bColorless;
		if (ColorOpts == 0) //all ok
		{
			if (wh && bu && bl && rd && gr && cls)
			{
				//nothing more
			}
			else
			{
				//get all results accept false ones
				if (!wh)
				{
					str += " AND Card.White=false";
				}
				if (!bu)
				{
					str += " AND Card.Blue=false";
				}
				if (!bl)
				{
					str += " AND Card.Black=false";
				}
				if (!rd)
				{
					str += " AND Card.Red=false";
				}
				if (!gr)
				{
					str += " AND Card.Green=false";
				}
				if (!cls)
				{
					str += " AND (Card.White=true OR Card.Blue=true OR Card.Black=true OR Card.Red=true OR Card.Green=true)";
				}
			}
		}
		else if (ColorOpts == 2) //mono only
		{
			if (wh && bu && bl && rd && gr && cls)
			{
				//nothing more
			}
			else
			{
				//get all results accept false ones
				if (!wh)
				{
					str += " AND Card.White=false";
				}
				if (!bu)
				{
					str += " AND Card.Blue=false";
				}
				if (!bl)
				{
					str += " AND Card.Black=false";
				}
				if (!rd)
				{
					str += " AND Card.Red=false";
				}
				if (!gr)
				{
					str += " AND Card.Green=false";
				}
			}
			
			str += " AND (";
			if (wh)//white=true, u/b/r/g=false
			{
				str += "(Card.White=true AND Card.Blue=false AND Card.Black=false AND Card.Red=false AND Card.Green=false)";
				flag = true;
			}
			if (bu)//blue=true, w/b/r/g=false
			{
				if (flag) { str += " OR "; }
				str += "(Card.Blue=true AND Card.White=false AND Card.Black=false AND Card.Red=false AND Card.Green=false)";
				flag = true;
			}
			if (bl)//black=true, w/u/r/g=false
			{
				if (flag) { str += " OR "; }
				str += "(Card.Black=true AND Card.White=false AND Card.Blue=false AND Card.Red=false AND Card.Green=false)";
				flag = true;
			}
			if (rd)//red=true, w/u/b/g=false
			{
				if (flag) { str += " OR "; }
				str += "(Card.Red=true AND Card.White=false AND Card.Blue=false AND Card.Black=false AND Card.Green=false)";
				flag = true;
			}
			if (gr)//green=true, w/u/b/r=false
			{
				if (flag) { str += " OR "; }
				str += "(Card.Green=true AND Card.White=false AND Card.Blue=false AND Card.Black=false AND Card.Red=false)";
				flag = true;
			}
			str += ")";
			flag = false;
		}
		else if (ColorOpts == 1) //multi only
		{
			if (wh && bu && bl && rd && gr)
			{
				//nothing more
			}
			else
			{
				//get all results accept false ones
				if (!wh)
				{
					str += " AND Card.White=false";
				}
				if (!bu)
				{
					str += " AND Card.Blue=false";
				}
				if (!bl)
				{
					str += " AND Card.Black=false";
				}
				if (!rd)
				{
					str += " AND Card.Red=false";
				}
				if (!gr)
				{
					str += " AND Card.Green=false";
				}
			}
			
			str += " AND (";
			if (wh)//white=true, at least one of u/b/r/Card.Green=true
			{
				str += "(Card.White=true AND (Card.Blue=true OR Card.Black=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (bu)//blue=true, at least one of w/b/r/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Blue=true AND (Card.White=true OR Card.Black=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (bl)//black=true, at least one of u/w/r/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Black=true AND (Card.White=true OR Card.Blue=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (rd)//red=true, at least one of u/b/w/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Red=true AND (Card.White=true OR Card.Blue=true OR Card.Black=true OR Card.Green=true))";
				flag = true;
			}
			if (gr)//green=true, at least one of u/b/r/Card.White=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Green=true AND (Card.White=true OR Card.Blue=true OR Card.Black=true OR Card.Red=true))";
				flag = true;
			}
			str += ")";
			flag = false;
		}
		else if (ColorOpts == 3) //hybrid only
		{
			if (wh && bu && bl && rd && gr)
			{
				//nothing more
			}
			else
			{
				//get all results accept false ones
				if (!wh)
				{
					str += " AND Card.White=false";
				}
				if (!bu)
				{
					str += " AND Card.Blue=false";
				}
				if (!bl)
				{
					str += " AND Card.Black=false";
				}
				if (!rd)
				{
					str += " AND Card.Red=false";
				}
				if (!gr)
				{
					str += " AND Card.Green=false";
				}
			}
			str += " AND Card.CastingCost LIKE '%(_/_)%' ";
			
			str += " AND (";
			if (wh)//white=true, at least one of u/b/r/Card.Green=true
			{
				str += "(Card.White=true AND (Card.Blue=true OR Card.Black=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (bu)//blue=true, at least one of w/b/r/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Blue=true AND (Card.White=true OR Card.Black=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (bl)//black=true, at least one of u/w/r/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Black=true AND (Card.White=true OR Card.Blue=true OR Card.Red=true OR Card.Green=true))";
				flag = true;
			}
			if (rd)//red=true, at least one of u/b/w/Card.Green=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Red=true AND (Card.White=true OR Card.Blue=true OR Card.Black=true OR Card.Green=true))";
				flag = true;
			}
			if (gr)//green=true, at least one of u/b/r/Card.White=true
			{
				if (flag) { str += " OR "; }
				str += "(Card.Green=true AND (Card.White=true OR Card.Blue=true OR Card.Black=true OR Card.Red=true))";
				flag = true;
			}
			str += ")";
			flag = false;
		}
		else if (ColorOpts == 0) //hybridMonoCastableOnly
		{
			string[] wubrg={"","","","",""};
			string[] colors={"White","Blue","Black","Red","Green"};
			
			if (wh)
			{
				wubrg[0]="W";
			}
			if (bu)
			{
				wubrg[1]="U";
			}
			if (bl)
			{
				wubrg[2]="B";
			}
			if (rd)
			{
				wubrg[3]="R";
			}
			if (gr)
			{
				wubrg[4] = "G";
			}
			
			flag = false;    
			str += " AND (";
			for(int i=0;i<5;i++)
			{
				if(wubrg[i]!="")
				{
					if (flag)
					{
						str += " OR ";
					}
					flag = true;
					str += "(";
					str += "((Card.CastingCost LIKE '%(_/" + wubrg[i] + ")%' OR Card.CastingCost LIKE '%(" + wubrg[i] + "/_)%')";
					str += "AND Card.CastingCost NOT LIKE '%)[!" + wubrg[i] + "]%')";
					str += " OR (";
					for(int j=0;j<5;j++)
					{
						if(j!=0)
						{
							str += "AND ";
						}
						str +="Card."+colors[j]+"="+(i==j).ToString()+" ";
					}
					str += "))";
				}
			}
			str += ")";
		}
		
		return str;
	}

	private string ParseAndOrsORoutputNormal(string search,string field)
	{
		string[] OrSeperatedString, AndSeperatedString;
		string q =  " AND";
		bool flag = false;
		
		if (!search.Contains("&") && !search.Contains("|"))
		{
			if(search.Contains("!"))
			{
				q += " " + field + " NOT LIKE '%" + search.Replace("!", "") + "%' ";
			}
			else
			{
				q += " " + field + " LIKE '%" + search +"%' ";
			}
		}
		else // split on and/or's
		{
			
			OrSeperatedString = search.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			q += " (";
			for (int i = 0; i < OrSeperatedString.Length; i++)
			{
				AndSeperatedString = OrSeperatedString[i].Trim().Split("&".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
				if (AndSeperatedString.Length != 0)
				{
					
					if (flag)
					{
						q += " OR";
					}
					flag = true;
					q += " (";
					
					for (int j = 0; j < AndSeperatedString.Length; j++)
					{
						AndSeperatedString[j] = AndSeperatedString[j].Trim();
						if (j != 0)
						{
							q += " AND";
						}
						
						if (AndSeperatedString[j].Contains("!"))
						{
							q += " " + field + " NOT LIKE '%" + AndSeperatedString[j].Replace("!", "") + "%' ";
						}
						else
						{
							q += " " + field + " LIKE '%" + AndSeperatedString[j] + "%' ";
						}
					}
					
					q += ")";
				}
				
			}
			q += ")";
		}
		
		return q;
	}

	public int StrToInt(string src)
	{
		int total = 0;
		if (src == "" || src == "0")
		{
			return total;
		}
		for (int i = 0; i < src.Length; i++)//5124
		{
			total += (src[i] - 48) * (int)Mathf.Pow((float)10, (float)src.Length - 1 - (float)i);
		}
		return total;
	}
}
