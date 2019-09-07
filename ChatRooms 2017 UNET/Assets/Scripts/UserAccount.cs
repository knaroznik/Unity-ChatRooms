using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserAccount{
	private static string p_username = "notSet";

	public static void defaultUserName(){
		p_username = "notSet";
	}

	public static bool isDefault(){
		if (p_username == "notSet") {
			return true;
		}
		return false;
	}

	public static void SetUserName(string l_input){
		p_username = l_input;
	}

	public static string GetUserName(){
		return p_username;
	}
}
