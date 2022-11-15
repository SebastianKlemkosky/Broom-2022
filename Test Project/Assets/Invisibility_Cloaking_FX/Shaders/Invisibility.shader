Shader "Custom/Invisibility"
{
	Properties
	{
		_MainTex ("", 2D) = "white" {}
		_Tint ("Tint (RGB)", Color) = (0.5,0.5,0.5,1)
		_IntensityAndScrolling ("Intensity (XY); Scrolling (ZW)", Vector) = (0.1,0.1,1,1)
		_DistanceFade ("Distance Fade (X=Near, Y=Far, ZW=Unused)", Float) = (20, 50, 0, 0)
	}

	SubShader
	{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True"}
		Blend One Zero
		Lighting Off
		Fog { Mode Off }
		ZWrite Off
		LOD 200
		Cull [_CullMode]

		GrabPass{ "_GrabTexture" }
	
		Pass
		{  
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma shader_feature MASK
				#pragma shader_feature MIRROR_EDGE
				#pragma shader_feature DEBUGUV
				#pragma shader_feature DEBUGDISTANCEFADE

				#define ENABLE_TINT 1
				#include "UnityCG.cginc"
				#include "Invisibility.cginc"
			ENDCG
		}
	}
}
