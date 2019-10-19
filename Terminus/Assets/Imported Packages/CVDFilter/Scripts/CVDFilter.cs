using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class CVDFilter : MonoBehaviour {
	enum ColorType { Normal, Protanopia, Protanomaly, Deuteranopia, Deuteranomaly, Tritanopia, Tritanomaly, Achromatopsia, Achromatomaly }

    #region CVD Filter

    [SerializeField] ColorType visionType = ColorType.Normal;
	ColorType currentVisionType;
	PostProcessProfile[] profiles;
	PostProcessVolume postProcessVolume;

	void Start () {
		currentVisionType = visionType;
		gameObject.layer = LayerMask.NameToLayer("CVDFilter");
		SetupVolume();
		LoadProfiles();
		ChangeProfile();
	}

	void Update () {
		if (visionType != currentVisionType) {
			currentVisionType = visionType;
			ChangeProfile();
		}

        // on input, change target vision type
        // F1: Reset, F2: Previous Type, F3: Next Type
        if (Input.GetKeyDown(KeyCode.F1))
            visionType = ColorType.Normal;
        else if (Input.GetKeyDown(KeyCode.F2))
            visionType = (ColorType)Mathf.Max(0, (int)currentVisionType - 1);
        else if (Input.GetKeyDown(KeyCode.F3))
            visionType = (ColorType)Mathf.Min(System.Enum.GetNames(typeof(ColorType)).Length - 1, (int)currentVisionType + 1);
    }

	void SetupVolume () {
		postProcessVolume = GetComponent<PostProcessVolume>();
		postProcessVolume.isGlobal = true;
	}

	void LoadProfiles () {
		Object[] profileObjects = Resources.LoadAll("", typeof(PostProcessProfile));
		profiles = new PostProcessProfile[profileObjects.Length];
		for (int i = 0; i < profileObjects.Length; i++) {
			profiles[i] = (PostProcessProfile)profileObjects[i];
		}
	}

	void ChangeProfile () {
		postProcessVolume.profile = profiles[(int)currentVisionType];
	}

    #endregion

    #region Singleton

    private static CVDFilter instance;     // local singleton instance variable

    /// <summary>
    /// Read-access property returning instance of CVD filter instance
    /// </summary>
    public static CVDFilter Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Use for initialization
    /// </summary>
    void Awake()
    {
        // if singleton has already been initialized as another instance
        if (instance != null && Instance != this)
        {
            // destroy this instance
            Destroy(gameObject);
            return;
        }

        // otherwise, set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

}
