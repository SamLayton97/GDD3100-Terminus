using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class CVDFilter : MonoBehaviour {
	enum ColorType { Normal, Protanopia, Protanomaly, Deuteranopia, Deuteranomaly, Tritanopia, Tritanomaly, Achromatopsia, Achromatomaly }

    [SerializeField] ColorType visionType = ColorType.Normal;
	ColorType currentVisionType;
	[SerializeField] PostProcessProfile[] profiles;
	PostProcessVolume postProcessVolume;

	void Start () {
		currentVisionType = visionType;
		gameObject.layer = LayerMask.NameToLayer("PostProcessing");
		SetupVolume();
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

	void ChangeProfile () {
		postProcessVolume.profile = profiles[(int)currentVisionType];
	}

}
