DEMO SCENE:
You can use the arrow keys to move the character sprite and see how it fits perfectly in the pixel grid.
Try changing the zoom in the camera for scaled pixel perfect sprites.

QUICK START GUIDE:

1. Import "Pixel Perfect" package to your project

2. Replace your game camera with the provided "PixelPerfectCamera" prefab

3. For additional pixel snap, add the "PixelPerfectSprite" behaviour to you sprites or "PixelPerfectQuad" if you are using quads.

4. Set the "PixelsPerUnit" value to whatever are you using in your sprite import settings.

5. Adjust the camera "zoom" slider in the inspector if you want to scale up your sprites.

6. Adjust the scale property of the PixelPerfect components at will.

ANCHORS:

1. Add "PixelPerfectSprite" or "PixelPerfectQuad" component to the anchored element (health bars etc...).

2. Check that you don't have both the anchor and non-anchor components in the object
(e.g: PixelPerfectSprite + PixelPerfectSpriteAnchor = wrong)

3. Assign the Anchor Camera field to the camera you want to anchor to.

4. Pick an anchor position and if the elements have to zoom with the camera.

Note: if you are using a custom sprite material, be sure to uncheck the "Pixel Snap" option.