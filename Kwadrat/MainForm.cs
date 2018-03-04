using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;

namespace Kwadrat
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		private Microsoft.DirectX.DirectInput.Device keyb;
		private Microsoft.DirectX.DirectInput.Device mouse;
		// private ArrayList objList = new ArrayList();
		private bool dxEnable = false;
		private bool bResized = false;
		private Matrix worldMatrix=Matrix.RotationYawPitchRoll(0,0,0);
		internal bool bReset = false;
		internal bool bDrawObj = false;
		internal BoxObject3D obj=null;
		internal Microsoft.DirectX.Direct3D.Device device=null;
		private int x=0,y=0,z=0;
		ToolForm toolform;

		// temporary to see what ever


		#region My Lib Library
		static private VertexBuffer lineList_VB = null;
		static CustomVertex.PositionColored[] 	verts = new CustomVertex.PositionColored[2];



		static public void LineTo(ref Microsoft.DirectX.Direct3D.Device device, Vector3 v1, Vector3 v2, Color color)
		{
			// Line #1

			verts[0].Position = v1;
			verts[0].Color = color.ToArgb();
			verts[1].Position = v2;
			verts[1].Color = color.ToArgb();
			lineList_VB.SetData( verts, 0, LockFlags.None );

			device.VertexFormat = CustomVertex.PositionColored.Format;
			device.RenderState.Lighting = false;
			device.RenderState.PointSize = 4.0f;
			device.SetStreamSource(0, lineList_VB, 0); 
			device.DrawPrimitives(PrimitiveType.LineList, 0, 2);      
			device.RenderState.Lighting = true;
		}
		#endregion



		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// all in onPaint
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

			dxEnable = false;
			InitializeDevice();
			InitializeLights();
			InitializeCamera();
			InitializeObjects();	
			dxEnable = true;

			InitializeKeyboard();
			InitializeMouse();

		   }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 507);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "symulacja kwadratu trzymanego na rogu";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		#region Initialize all my things

		private void InitializeKeyboard()
		{
			keyb = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
			keyb.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
			keyb.Acquire();
		}

		private void InitializeMouse()
		{
			mouse = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Mouse);
			mouse.SetCooperativeLevel(this, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background );
			mouse.Properties.AxisModeAbsolute = false;
			mouse.Acquire();
		}

		private void InitializeDevice()
		{
			PresentParameters presentParams = new PresentParameters();
			presentParams.Windowed = true;
			presentParams.SwapEffect = SwapEffect.Discard;
			presentParams.EnableAutoDepthStencil = true;   //with depth buffer
			presentParams.AutoDepthStencilFormat = DepthFormat.D24X8; //16 bit depth

			if ( device != null ) device.Dispose();

			device = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, presentParams);
			// device.RenderState.FillMode = FillMode.WireFrame;
			device.RenderState.CullMode = Cull.None;
			device.Transform.World=worldMatrix;


			lineList_VB = new VertexBuffer( typeof(CustomVertex.PositionColored),
				2, device,
				Usage.Dynamic | Usage.WriteOnly,
				CustomVertex.PositionColored.Format,
				Pool.Default );
		}

		private void InitializeLights()
		{
			device.Lights[0].Type = LightType.Directional;
			device.Lights[0].Diffuse = Color.White;
			device.Lights[0].Direction = new Vector3( 0, 0, 5 );
			device.Lights[0].Enabled = true;
			device.RenderState.Lighting = true;
		}

		private void InitializeCamera()
		{
			device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI/4, this.Width/this.Height, 1f, 100f);
			device.Transform.View = Matrix.LookAtLH(new Vector3(0,0,-5), new Vector3(0,0,0), new Vector3(0,1,0));
		}

		private void InitializeObjects()
		{
			obj = new BoxObject3D(device, 1, 1, 1);
			obj.Translate(-0.5f,-0.5f,-0.5f);
		}

		#endregion

		private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if ( dxEnable ) 
				{
					device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black , 1.0f, 0);
					device.BeginScene();

					obj.Draw(device, worldMatrix);
					device.Transform.World=worldMatrix;
					LineTo(ref device, new Vector3(0,0,-1), new Vector3(0,0,1), Color.Blue);
					LineTo(ref device, new Vector3(1,0,-1), new Vector3(1,0,1), Color.Blue);
					LineTo(ref device, new Vector3(-1,0,-1), new Vector3(-1,0,1), Color.Blue);
					LineTo(ref device, new Vector3(0.5f,0,-1), new Vector3(0.5f,0,1), Color.Blue);
					LineTo(ref device, new Vector3(-0.5f,0,-1), new Vector3(-0.5f,0,1), Color.Blue);

					LineTo(ref device, new Vector3(0,-1,0), new Vector3(0,1,0), Color.Red);
					LineTo(ref device, new Vector3(-1,0,0), new Vector3(1,0,0), Color.Blue);
					LineTo(ref device, new Vector3(-1,0,1), new Vector3(1,0,1), Color.Blue);
					LineTo(ref device, new Vector3(-1,0,0.5f), new Vector3(1,0,0.5f), Color.Blue);
					LineTo(ref device, new Vector3(-1,0,-1), new Vector3(1,0,-1), Color.Blue);
					LineTo(ref device, new Vector3(-1,0,-0.5f), new Vector3(1,0,-0.5f), Color.Blue);

					device.EndScene();
					device.Present();
					this.Invalidate();

					if ( bDrawObj ) 
					{
						obj.Step();
						toolform.label6.Text = ""+obj.PotentialEnergy;
						toolform.label14.Text = ""+(obj.PotentialEnergy+obj.KineticEnergyW);
						toolform.label13.Text = ""+obj.KineticEnergyW;
					}



					ReadKeyboard();
					UpdateInputState();
				}
				if ( bResized ) 
				{
					bResized = false;
					dxEnable = false;
					InitializeDevice();
					InitializeLights();
					InitializeCamera();
					InitializeObjects();	
					dxEnable = true;
				}
			} 
			catch ( Exception er ) 
			{
				MessageBox.Show(er.Message);
			}
		}

		private void UpdateInputState()
		{
			// Check the mouse state
			MouseState state = mouse.CurrentMouseState;

			// Buttons
			byte[] buttons = state.GetMouseButtons();

			if ( buttons[1] != 0 ) 
			{
				worldMatrix = Matrix.RotationYawPitchRoll((x-state.X)*0.01f, 0, 0)* worldMatrix;
				worldMatrix = Matrix.RotationYawPitchRoll(0, (y-state.Y)*0.01f, 0)* worldMatrix;
			}
			else if ( buttons[2] != 0 ) 
				worldMatrix = worldMatrix * Matrix.Translation(0, 0, (z-state.Z)*0.005f);
			else 
			{
				x=state.X;
				y=state.Y;
				z=state.Z;
			}
		}


		private void ReadKeyboard()
		{

			KeyboardState keys = keyb.GetCurrentKeyboardState();

			if (keys[Key.Delete])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(0.03f, 0, 0)* worldMatrix;
			}
			if (keys[Key.Next])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(-0.03f, 0, 0)* worldMatrix;
			} 
			if (keys[Key.Home])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(0, 0.03f, 0)* worldMatrix;
			}
			if (keys[Key.End])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(0, -0.03f, 0)* worldMatrix;
			} 
			if (keys[Key.Insert])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(0, 0, 0.03f)* worldMatrix;
			}
			if (keys[Key.PageUp])
			{
				worldMatrix = Matrix.RotationYawPitchRoll(0, 0, -0.03f) * worldMatrix;
			} 

			if (keys[Key.W])
			{
				worldMatrix = worldMatrix * Matrix.Translation(0, 0, 0.2f);
			}
			if (keys[Key.S])
			{
				worldMatrix = worldMatrix * Matrix.Translation(0, 0, -0.2f);
			} 
			if (keys[Key.Space])
			{

				//foreach (Object3D obj in objList) 
				//	obj.Step();
			}
		}

		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			bResized = true;
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			toolform = new ToolForm(this);
			toolform.Show();			
		}

	}
}
