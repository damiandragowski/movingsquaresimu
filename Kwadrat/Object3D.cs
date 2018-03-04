using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D=Microsoft.DirectX.Direct3D;

namespace Kwadrat
{
	/// <summary>
	/// Summary description for Object3D.
	/// </summary>
	public class Object3D
	{
		private Mesh mesh=null;
		private Direct3D.Material[] meshMaterials=null; // Materials for our mesh
		private Texture[] meshTextures=null; // Textures for our mesh
		private Direct3D.Material material;
		protected Matrix matrix=Matrix.Identity;
		protected Quaternion quat;


		public Object3D(Device device)
		{
			material = new Direct3D.Material();
			material.Diffuse = material.Ambient = Color.White;
			mesh = Mesh.Box(device, 1, 1, 1);
			quat = new Quaternion();
		}

		public Object3D(Device device, float a, float b, float c)
		{
			material = new Direct3D.Material();
			material.Diffuse = material.Ambient = Color.FromArgb(0,0,255,100);
			mesh = Mesh.Box(device, a, b, c);
			quat = new Quaternion();
		}


		public void RotateYawPitchRoll(float yaw, float pitch, float roll)
		{
			matrix = matrix * Matrix.RotationYawPitchRoll(yaw, pitch, roll);
		}

		public void Translate(float x, float y, float z)
		{
			matrix = matrix * Matrix.Translation(x, y, z);
		}

		virtual public void Draw(Device device, Matrix worldMatrix)
		{
			device.Material = material;
			if ( mesh  != null ) 
			{
				
					device.Transform.World = (matrix * Matrix.RotationQuaternion(quat)) * worldMatrix;
					mesh.DrawSubset(0);
			}
		}

		public void Dispose()
		{
			if ( mesh != null ) { mesh.Dispose(); mesh=null; }
			if ( meshMaterials != null ) meshMaterials=null;
			if ( meshTextures != null ) meshTextures=null;
		}

		virtual public void PositionGrav()
		{
		}
		virtual public void Step()
		{
		}

	}
}
