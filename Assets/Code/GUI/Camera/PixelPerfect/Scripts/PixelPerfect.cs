using UnityEngine;
using System.Collections;

public static class PixelPerfect {
	public static int pixelScale;
	public static int pixelsPerUnit=16;
	public static float unitsPerPixel=0.0625f;
	public static float pixelOffset;
	
	public static void SetPixelPerfect(int importPixelsPerUnit, int pixelZoom) {
		pixelsPerUnit=importPixelsPerUnit;
		unitsPerPixel=(1f/(float)pixelsPerUnit);
		pixelScale=pixelZoom;
		if (pixelZoom%2!=0) {
			pixelOffset=0.25f;
		} else {
			pixelOffset=0.33f;
		}
	}

	public static Vector2 GetMainGameViewSize() {
		return new Vector2(Screen.width, Screen.height);
//		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
//		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
//		System.Object Res = GetSizeOfMainGameView.Invoke(null,null);
//		return (Vector2)Res;
	}
}
