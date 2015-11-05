using UnityEngine;
using System.Collections;
using System.Data;

public class Result
{
	public int Set_id = -1;
	public int Card_id = -1;
	public int CardNumber = -1;
	public string TypeLine="";
	public string CardName="";
	public string CardText="";
	public string CastingCost="";
	public string Power="";
	public string Toughness="";
	public bool bWhite=true;
	public bool bBlue=true;
	public bool bBlack=true;
	public bool bRed=true;
	public bool bGreen=true;
	public int ConvertedManaCost=0;
	public string SetName="";
	public string Rarity="";
	public string ImagePath="";
	public string FlavorText="";
	public string  multiverse_id= "";
	public string Artist="";
	public int  isSplit= 0;
	public string splitName="";
	public Texture2D texture = null; //set for faster lookup by the display controller
	public Sprite sprite = null; //set for faster lookup by the display controller
	public int quantity = 0; //to use for single player mode eventually



	public Result(IDataReader reader)
	{
		Set_id = reader.GetInt32 (0);
		Card_id = reader.GetInt32 (1);
		CardNumber = reader.GetInt32 (2);
		TypeLine = reader.GetString (3);
		CardName = reader.GetString (4);
		CardText = reader.GetString (5);
		CastingCost = reader.GetString (6);
		Power = reader.GetString (7);
		Toughness = reader.GetString (8);
		bWhite = reader.GetBoolean (9);
		bBlue = reader.GetBoolean (10);
		bBlack = reader.GetBoolean (11);
		bRed = reader.GetBoolean (12);
		bGreen = reader.GetBoolean (13);
		ConvertedManaCost = reader.GetInt32 (14);
		SetName = reader.GetString (15);
		Rarity = reader.GetString (16);
		ImagePath = reader.GetString (17);
		FlavorText = reader.GetString (18);
		multiverse_id = reader.GetString (19);
		Artist = reader.GetString (20);
		isSplit = reader.GetInt32 (21);
		splitName = reader.GetString (22);
	}
}
