using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class PixelPerfectQuadAnchor : MonoBehaviour {
	public PixelPerfectCamera anchorCamera;
	public AnchorType anchorType=AnchorType.UpperLeft;
	public bool zoomsWithCamera=true;

	public void Update () {
		if (zoomsWithCamera && PixelPerfect.pixelScale!=0) {
			transform.localScale=Vector3.one/(float)PixelPerfect.pixelScale;
		}
		Vector2 quadSize=new Vector2(transform.localScale.x, transform.localScale.y);
		Vector2 size=(PixelPerfect.GetMainGameViewSize()*0.5f)*PixelPerfect.unitsPerPixel/PixelPerfect.pixelScale-quadSize/2;
		Vector3 cameraPosition=new Vector3(anchorCamera.transform.position.x, anchorCamera.transform.position.y, 0);
		int i=0, j=0;
		switch (anchorType) {
		case AnchorType.UpperLeft:    i=-1; j=1;  break;
		case AnchorType.UpperMiddle:  i= 0; j=1;  break;
		case AnchorType.UpperRight:   i= 1; j=1;  break;
		case AnchorType.MiddleLeft:   i=-1; j=0;  break;
		case AnchorType.MiddleCenter: i= 0; j=0;  break;
		case AnchorType.MiddleRight:  i= 1; j=0;  break;
		case AnchorType.LowerLeft:    i=-1; j=-1; break;
		case AnchorType.LowerCenter:  i= 0; j=-1; break;
		case AnchorType.LowerRight:   i= 1; j=-1; break;
		}
		Vector3 newPos=cameraPosition+Vector3.right*size.x*i+Vector3.up*size.y*j;
		if (Mathf.Abs(newPos.x)<float.MaxValue) {
			transform.position=newPos;
			if (!zoomsWithCamera) {
				transform.position+=(Vector3.up-Vector3.right)*PixelPerfect.pixelOffset*PixelPerfect.unitsPerPixel;
			}
		}
	}
}

public enum AnchorType {UpperLeft, UpperMiddle, UpperRight, MiddleLeft, MiddleCenter, MiddleRight, LowerLeft, LowerCenter, LowerRight}