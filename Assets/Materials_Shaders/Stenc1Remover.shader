// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Stenc1Remover"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		//[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Geometry"
				//"IgnoreProjector" = "True"
				"RenderType" = "Opaque"
				//"PreviewType" = "Plane"
				//"CanUseSpriteAtlas" = "True"
				
			}
			ColorMask 0
			LOD 200
			Stencil
			{
				Ref 1
				Comp Always
				Pass Replace
			}

			Cull Back
			Lighting Off
			ZTest Less
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				struct appdata
				{
					float4 vertex   : POSITION;
				};

				struct v2f
				{
					float4 pos   : SV_POSITION;
				};

				fixed4 _Color;

				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);

					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					return half4(0, 0, 0, 0);
				}
					ENDCG
			}
		}
}
