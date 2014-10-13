using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class PixelPerfectSpriteAnchor : MonoBehaviour {
	public PixelPerfectCamera anchorCamera;
	public AnchorType anchorType=AnchorType.UpperLeft;
	public bool zoomsWithCamera=true;

	SpriteRenderer spriteRenderer;

	public void Update () {
		if (spriteRenderer==null) {spriteRenderer=GetComponent<SpriteRenderer>();}
		Vector2 spriteSize=new Vector2(spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height)*PixelPerfect.unitsPerPixel*0.5f;
		if (zoomsWithCamera && PixelPerfect.pixelScale!=0) {
			spriteSize=spriteSize/(float)PixelPerfect.pixelScale;
			transform.localScale=Vector3.one*1f/(float)PixelPerfect.pixelScale;
		} else {
			transform.localScale=Vector3.one;
		}
		Vector2 size=(PixelPerfect.GetMainGameViewSize()*0.5f)*PixelPerfect.unitsPerPixel/PixelPerfect.pixelScale-spriteSize;
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