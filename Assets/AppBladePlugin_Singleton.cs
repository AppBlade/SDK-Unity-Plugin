using UnityEngine;
using System.Collections;


//Singleton class for AppBlade Functionality. 
public class AppBladePlugin_Singleton : MonoBehaviour 
{ 
	#if UNITY_IPHONE
	   [DllImport ("__Internal")]
		private static extern float registerAppBlade (char *optionsString);
		private static extern float setCrashReporting (char *optionsString);
		private static extern float setFeedbackReporting (char *optionsString);
		private static extern float notifyCrash (char *optionsString);
		private static extern float promptFeedback (char *optionsString);
	//		UnitySendMessage("AppBlade", "appBladeRegistered", "");
	#elif UNITY_ANDROID
	   [DllImport ("AppBladeNativePlugin")]
   #endif
	
	private static AppBladePlugin_Singleton instance;
	private AppBladePlugin_Singleton() {}
	public static AppBladePlugin_Singleton Instance
	{ 
	get { if (instance == null) { instance = new AppBladePlugin_Singleton(); } return instance; }
	}
	
	public string projectUUID = "";
	public string projectToken = "";
	public string projectSecret = "";
	public string projectIssuedAt = "";
	public bool feedbackEnabled = false;
	public bool crashReportEnabled = false;
	public bool crashReportLevel = false;
	
	void setupPlugin()
	{
		//register appBlade variables
		//check variables in the editor settings for what we want enabled
		
	}
	
	bool isValid()
	{
		//check if we have registered with AppBlade variables (setupPlugin needs to be called)
		return (projectUUID.Length != 0) && 
			(projectToken.Length != 0) &&
			(projectSecret.Length != 0) && 
			(projectIssuedAt.Length != 0);
	}
	
	void enableCrashReporting()
	{
	
	}
	
	void enableFeedbackReporitng()
	{
	
	}
	
	void startSession()
	{
	
	}
	
	void endSession()
	{
	
	}
	
	
	//Error here in the documented implementation, OnEnable will be called before Awake in certain cases.
	void OnEnable()
	{
		if(!this.isValid())
			AppBladePlugin_Singleton.Instance.setupPlugin();
	}
	void Awake()
	{
		if(!this.isValid())
			AppBladePlugin_Singleton.Instance.setupPlugin();
	}
	
	// Use this for initialization (But don't really.)
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
