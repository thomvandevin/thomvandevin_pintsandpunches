using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectSprite : MonoBehaviour {

	[Range(1,10)]public int scale=1;
	public bool onlyAtStart=false;
	Vector3 fixedPosition, positionOffset, spriteOddOffset;
	float grid;
	SpriteRenderer spriteRenderer;

	public void Start() {
		Update();
	}

	public void Update() {
		if (onlyAtStart && Application.isPlaying) {
			Destroy(this);
		}
		if (spriteRenderer==null) {
			spriteRenderer=GetComponent<SpriteRenderer>();
		}
		spriteOddOffset=new Vector3((spriteRenderer.sprite.rect.width%2==0)?0:0.5f,(spriteRenderer.sprite.rect.height%2==0)?0:0.5f,0);
		Transform saveParent=transform.parent;
		transform.parent=null;
		transform.localScale=new Vector3(Mathf.Sign(transform.localScale.x)*scale,Mathf.Sign(transform.localScale.y)*scale,Mathf.Sign(transform.localScale.z)*scale);
		transform.parent=saveParent;
	
		fixedPosition=new Vector3(
			Mathf.Round((transform.position.x+positionOffset.x)/PixelPerfect.unitsPerPixel/scale)*PixelPerfect.unitsPerPixel*scale,
			Mathf.Round((transform.position.y+positionOffset.y)/PixelPerfect.unitsPerPixel/scale)*PixelPerfect.unitsPerPixel*scale,
			Mathf.Round((transform.position.z+positionOffset.z)/PixelPerfect.unitsPerPixel/scale)*PixelPerfect.unitsPerPixel*scale);
		fixedPosition+=(Vector3.up-Vector3.right)*PixelPerfect.pixelOffset*PixelPerfect.unitsPerPixel+spriteOddOffset*PixelPerfect.unitsPerPixel;

		if (Vector3.Distance(transform.position, fixedPosition)>0) {
			positionOffset+=transform.position-fixedPosition;
			transform.position=fixedPosition;
		}
	}
	
}
