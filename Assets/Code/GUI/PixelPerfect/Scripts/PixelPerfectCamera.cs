using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

	public int pixelsPerUnit=16;
	[Range(1,16)]public int pixelZoom=1;
	public bool stickToPixelGrid=false;

	[HideInInspector]public int checkedPixelZoom;

	Vector3 fixedPosition;
	Vector3 positionOffset;
	float grid;

	void Update () {
		AdjustSize();
		if (stickToPixelGrid) {
			AdjustPosition();
		}
		if (checkedPixelZoom!=pixelZoom) {
			AdjustPosition();
			foreach (var item in FindObjectsOfType<PixelPerfectSprite>())       {item.Update();}
			foreach (var item in FindObjectsOfType<PixelPerfectQuad>())         {item.Update();}
			foreach (var item in FindObjectsOfType<PixelPerfectSpriteAnchor>()) {item.Update();}
			foreach (var item in FindObjectsOfType<PixelPerfectQuadAnchor>())   {item.Update();}
		}
	}

	public void AdjustPosition() {
		fixedPosition=new Vector3(
			Mathf.Round((transform.position.x+positionOffset.x)/PixelPerfect.unitsPerPixel)*PixelPerfect.unitsPerPixel,
			Mathf.Round((transform.position.y+positionOffset.y)/PixelPerfect.unitsPerPixel)*PixelPerfect.unitsPerPixel,
			Mathf.Round((transform.position.z+positionOffset.z)/PixelPerfect.unitsPerPixel)*PixelPerfect.unitsPerPixel);
		
		if (Vector3.Distance(transform.position, fixedPosition)>0) {
			positionOffset+=transform.position-fixedPosition;
			transform.position=fixedPosition;
		}
		checkedPixelZoom=pixelZoom;
	}

	public void AdjustSize() {
		PixelPerfect.SetPixelPerfect(pixelsPerUnit, pixelZoom);
		float targetHeight;
		if (Application.isEditor) {
			targetHeight=PixelPerfect.GetMainGameViewSize().y;
		} else {
			targetHeight = Screen.height;
		}
		camera.orthographicSize = (float)(((double)targetHeight / (double)PixelPerfect.pixelsPerUnit / (double)PixelPerfect.pixelScale) * 0.5d);		
	}
}