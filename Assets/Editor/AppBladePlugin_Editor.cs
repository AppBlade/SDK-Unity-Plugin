using UnityEngine;
using UnityEditor;
using System.Collections;

public class AppBladePlugin_Editor : EditorWindow {
	bool appBladeSingletonExistsInScene;
	AppBladePlugin_Singleton currentSingleton;
	private string errorString = "";
	GUIStyle errorLabelStyle;
	
	bool crashReportToggle;
	static bool reportErrors = false;
	static bool reportExceptions;
	static bool reportWarnings;
	
	bool feedbackReportToggle;
	static bool feebackIncludeScreenshot = false; 
	
	[MenuItem("Window/AppBlade")]
	
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		AppBladePlugin_Editor window = (AppBladePlugin_Editor)EditorWindow.GetWindow(typeof(AppBladePlugin_Editor));
		window.title = "AppBlade";
		window.errorLabelStyle = new GUIStyle();
		window.errorLabelStyle.wordWrap = true;
		window.errorLabelStyle.normal.textColor = Color.red;
		window.minSize = new Vector2(300f, 250f);
	}
	
	void OnEnable(){	
		//try to find the variables in the AppBladePlugin_Singleton, if there is one. (There needs to be one.)
		currentSingleton = AppBladePlugin_Singleton.Instance;
	}	
	
	void OnFocus()
	{
		currentSingleton = AppBladePlugin_Singleton.Instance;
		appBladeSingletonExistsInScene = GameObject.Find("AppBlade");
		if(!appBladeSingletonExistsInScene)
		{
			errorString = "The AppBlade Object couldn't be found in your current Scene. \nWe'll need one of those.";
		}
	}
	
	void OnGUI()
	{
		if(appBladeSingletonExistsInScene)
		{
			GameObject appBladeObject = GameObject.Find("AppBlade");
			AppBladePlugin_Singleton appBladeSingleton = (AppBladePlugin_Singleton)appBladeObject.GetComponent("AppBladePlugin_Singleton");
			GUILayout.Label ("Project API Keys (Required)", EditorStyles.boldLabel);
			appBladeSingleton.projectUUID = EditorGUILayout.TextField ("UUID", appBladeSingleton.projectUUID);
			appBladeSingleton.projectToken = EditorGUILayout.TextField ("Token", appBladeSingleton.projectToken);
			appBladeSingleton.projectSecret = EditorGUILayout.TextField ("Secret", appBladeSingleton.projectSecret);
			appBladeSingleton.projectIssuedAt = EditorGUILayout.TextField ("Issued At", appBladeSingleton.projectIssuedAt);
	
			crashReportToggle = EditorGUILayout.BeginToggleGroup ("Enable Crash Reporting", crashReportToggle);
				reportErrors = EditorGUILayout.Toggle( "Report Errors", reportErrors);
				reportExceptions = EditorGUILayout.Toggle( "Report Exceptions", reportExceptions);
				reportWarnings = EditorGUILayout.Toggle( "Report Warnings", reportWarnings);
			EditorGUILayout.EndToggleGroup ();
			
			feedbackReportToggle = EditorGUILayout.BeginToggleGroup ("Enable Feedback Reporting", feedbackReportToggle);
				feebackIncludeScreenshot = EditorGUILayout.Toggle( "Include Screenshot", feebackIncludeScreenshot);
			EditorGUILayout.EndToggleGroup ();
		}else{

			GUI.Label( new Rect(10,10, position.width - 20,90), errorString, errorLabelStyle);
			if ( GUI.Button( new Rect(10,70,150,30), "Make AppBlade Object") ){
		        Debug.Log("Making AppBlade Singleton");
				Vector3 pos = new Vector3(0,0,0); 
			    Quaternion rot = Quaternion.identity; 
				Object prefab = Resources.Load("Prefabs/AppBlade");
				if(prefab != null){
	     			Object appBladeInstance = Instantiate(prefab, pos, rot); 
					appBladeInstance.name = "AppBlade";
					appBladeSingletonExistsInScene = true;
				}else{
					errorString = "Could not locate the AppBlade prefab! Did you move it? \nPlease reimport the AppBlade library.";
				}
			}
		}
	
	}
}
