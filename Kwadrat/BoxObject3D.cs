using System;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D=Microsoft.DirectX.Direct3D;

namespace Kwadrat
{
	/// <summary>
	/// Summary description for RealObject3D.
	/// </summary>
	public class BoxObject3D: Object3D
	{
		#region Dane Klasy
		/// <summary>
		/// Dane do klasy
		/// </summary>
		private Vector3 vectDiagonal;
		private Vector3 gravVector = new Vector3(0,1,0);
		private Quaternion quatF = new Quaternion(0,0,0,0);
		private Vector3 T  =new Vector3(0,0,0); // moment obrotowy
		private Vector3 Center;
		private float Mass = 0f;
		private float roo = 1f;
		private Matrix InertiaTensor = Matrix.Identity;
		private Matrix InertiaTensorInv = Matrix.Identity;
		private System.Collections.Queue	points = new System.Collections.Queue(1000);
		private float gravitation=9.81f;
		private Vector3 omega = new Vector3(0,0,0); // predkosc katowa

		public  int psize=100;
		public  float	delta = 0.01f;		
		public Vector3 upperAxis;
		public float PotentialEnergy = 0;
		public float KineticEnergyW = 0;


		#endregion


		#region konsruktor
		public BoxObject3D(Device device, float a, float b, float c):base(device,a,b,c)
		{
			vectDiagonal = new Vector3(a,b,c);
			Mass = a * b * c * roo ;
			Center = new Vector3(a/2,b/2,c/2);

			setGrav(9.81f);
			// ustaw w kierunku grawitacji
			PositionGrav();
			CalcupperAxis();
			T = new Vector3(0,0,0);
		}

		#endregion

		#region dQ i dw

		public Quaternion dQ()
		{
			Quaternion w = new Quaternion(omega.X, omega.Y, omega.Z, 0);
			w = w*quat;
			return mulq(ref w,0.5f); // qt = w * q /2
		}


		public 	Vector3 dOmega()
		{
			// iwt = t + (iw)*w 
			return mulv(ref InertiaTensorInv , T + Vector3.Cross(mulv(ref InertiaTensor,omega), omega));
		}

		#endregion

		#region do mnoznia, cos o czym zapomnieli w m$
		public Vector3 mulv(ref Matrix mat, Vector3 v)
		{
			Vector3 res=new Vector3(0,0,0);
			res.X = mat.M11 * v.X + mat.M12 * v.Y + mat.M13 * v.Z;
			res.Y = mat.M21 * v.X + mat.M22 * v.Y + mat.M23 * v.Z;
			res.Z = mat.M31 * v.X + mat.M32 * v.Y + mat.M33 * v.Z;
			return res;
		}

		public Vector3 mulmv(ref Matrix mat, Vector3 v)
		{
			Vector3 res=new Vector3(0,0,0);
			res.X = mat.M11 * v.X + mat.M21 * v.Y + mat.M31 * v.Z;
			res.Y = mat.M12 * v.X + mat.M22 * v.Y + mat.M32 * v.Z;
			res.Z = mat.M13 * v.X + mat.M23 * v.Y + mat.M33 * v.Z;
			return res;
		}

		
		public void mul(ref Quaternion q, float d)
		{
			q.X*=d;
			q.Y*=d;
			q.Z*=d;
			q.W*=d;
		}

		public Quaternion mulq(ref Quaternion q, float d)
		{
			Quaternion qt = q;
			qt.X*=d;
			qt.Y*=d;
			qt.Z*=d;
			qt.W*=d;
			return qt;
		}

		#endregion

		public void setGrav(float grav)
		{
			float a=vectDiagonal.X, b=vectDiagonal.Y, c=vectDiagonal.Z;

			Mass = a * b * c * roo ;
			// macierz bezwladnosci
			/*
			InertiaTensor.M11 = ((b*b*b + c*c*c) * Mass ) / 3f;
			InertiaTensor.M22 = ((a*a*a + c*c*c) * Mass ) / 3f;
			InertiaTensor.M33 = ((a*a*a + b*b*b) * Mass ) / 3f;
			InertiaTensor.M12 = InertiaTensor.M21 = -(Mass * a * a * b * b) / 4f;
			InertiaTensor.M13 = InertiaTensor.M31 = -(Mass * a * a * c * c) / 4f;
			InertiaTensor.M23 = InertiaTensor.M32 = -(Mass * b * b * c * c) / 4f;
			*/


			InertiaTensor.M11 = ((b*b + c*c) * Mass ) / 3f;
			InertiaTensor.M22 = ((a*a + c*c) * Mass ) / 3f;
			InertiaTensor.M33 = ((a*a + b*b) * Mass ) / 3f;
			InertiaTensor.M12 = InertiaTensor.M21 = -(Mass * a * b) / 4f;
			InertiaTensor.M13 = InertiaTensor.M31 = -(Mass * a * c) / 4f;
			InertiaTensor.M23 = InertiaTensor.M32 = -(Mass * b * c) / 4f;
			// odwrotna
			InertiaTensorInv = Matrix.Invert(InertiaTensor);
			// sila grawitacji
			quatF.Y = Mass * grav;
			gravitation = grav;
		}

		public override void Draw(Microsoft.DirectX.Direct3D.Device device, Matrix worldMatrix)
		{
			base.Draw(device, worldMatrix);
			device.Transform.World = worldMatrix;
			if ( points.Count > 1 ) 
			{
				IEnumerator j = points.GetEnumerator();
				j.MoveNext();
				Vector3 temp=(Vector3)j.Current;
				for(int i=0; i < points.Count-1;++i) 
				{
					j.MoveNext();
					MainForm.LineTo(ref device, (Vector3)temp, (Vector3)j.Current, Color.Pink);
					temp=(Vector3)j.Current;
				}
			}
		}
		
		public override void Step()
		{
			float h=1f; //  Runge-Kutta

			Quaternion q0 = quat;
			Vector3 omega0 = omega;
			Quaternion quatrF;

			// reset przyspieszenia
			//T = new Vector3(0,0,0);

			Quaternion q1 = dQ();
			mul(ref q1, delta);
			Vector3   v1 = dOmega();
			v1 *= delta;

			quat = q0 + mulq(ref q1, (h/2f));
			quat.Normalize();
			omega = omega0 + v1*(h/2f);

			// liczymy aktualne przyspiesznie
			quatrF=(quat*quatF)*Quaternion.Conjugate(quat);
			T = Vector3.Cross(Center, new Vector3(quatrF.X,quatrF.Y,quatrF.Z)); // przyspieszenie w bryle


			Quaternion q2 = dQ();
			mul(ref q2, delta);
			Vector3   v2 = dOmega();
			v2 *= delta;

			quat = q0 + mulq(ref q2, (h/2f));
			quat.Normalize();
			omega = omega0 + v2*(h/2f);

			quatrF=(quat*quatF)*Quaternion.Conjugate(quat);
			T = Vector3.Cross(Center, new Vector3(quatrF.X,quatrF.Y,quatrF.Z));



			Quaternion q3 = dQ();
			mul(ref q3, delta);
			Vector3   v3 = dOmega();
			v3 *= delta;

			quat = q0 + q3;
			quat.Normalize();
			omega = omega0 + v3;

			quatrF=(quat*quatF)*Quaternion.Conjugate(quat);
			T = Vector3.Cross(Center, new Vector3(quatrF.X,quatrF.Y,quatrF.Z));


			Quaternion q4 = dQ();
			mul(ref q4, delta);
			Vector3   v4 = dOmega();
			v4 *= delta;

			quat = q0 + q4;
			quat.Normalize();
			omega = omega0 + v4;

			quatrF=(quat*quatF)*Quaternion.Conjugate(quat);
			T = Vector3.Cross(Center, new Vector3(quatrF.X,quatrF.Y,quatrF.Z));


			// end usrednianie wartosci

			Quaternion qsum = (q1 + mulq(ref q2,2f) + mulq(ref q3,2f) + q4);
			quat = q0 + mulq(ref qsum, h/6f);
			omega = omega0 + (v1 + 2*v2 + 2*v3 + v4)*(h/6f);
			quat.Normalize();

			quatrF=(quat*quatF)*Quaternion.Conjugate(quat);
			T = Vector3.Cross(Center, new Vector3(quatrF.X,quatrF.Y,quatrF.Z));

			AddPoint();
		}


		public void AddPoint()
		{
			Quaternion q1 = new Quaternion(-vectDiagonal.X, -vectDiagonal.Y, -vectDiagonal.Z, 0);
			q1 = (Quaternion.Conjugate(quat)*q1)*quat;
			while ( points.Count >= psize )
				points.Dequeue();
			points.Enqueue(new Vector3(q1.X, q1.Y, q1.Z));

			Quaternion q = new Quaternion(-Center.X, -Center.Y, -Center.Z, 0);
			q = (Quaternion.Conjugate(quat)*q)*quat;
			PotentialEnergy = (q.Y)*Mass*gravitation; // mgh centrum masy
			KineticEnergyW = Vector3.Dot( mulv(ref InertiaTensor, omega),omega)/2; // mv^2/2 + Iw^2 / 2 +  energia kinetyczna 
			// 0.5f*(Mass/gravitation) * Vector3.Dot(omega,omega)
		}

		#region ustawienia srodowiska
		public override void PositionGrav()
		{
			// kwaternionon najmniejszczego k¹ta
			Vector3 normDiag = Vector3.Normalize(vectDiagonal);
			Vector3 crossVector = Vector3.Cross(normDiag, gravVector);
			float square = (float)Math.Sqrt(( 1.0+Vector3.Dot(normDiag, gravVector) ) * 2.0);
			crossVector.Multiply(1f/square);
			quat.X = crossVector.X;
			quat.Y = crossVector.Y;
			quat.Z = crossVector.Z;
			quat.W = square/2; // ( cos(alfa/2) )
		}


		public void AxisRotate(Vector3 axis, float ang)
		{
			quat=Quaternion.RotationAxis(axis,ang)*quat;
		}


		public void CalcupperAxis()
		{
			Vector3 diag=new Vector3(vectDiagonal.X,vectDiagonal.Y,vectDiagonal.Z); 
			diag.Normalize();
			Quaternion q = new Quaternion(-diag.X, -diag.Y, -diag.Z, 0); // 90 stopni ,lewa oska
			q = quat * q *  Quaternion.Conjugate(quat);
			diag.X = q.X;
			diag.Y = q.Y;
			diag.Z = q.Z;
			upperAxis = Vector3.Cross(diag, gravVector);
			upperAxis.Normalize();
		}


		public void SetOmega(float ang) // uklad bryly
		{
			Vector3 v0=Vector3.Normalize(vectDiagonal);
			omega = ang*v0;
		}


		#endregion
	}
}
