using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D=Microsoft.DirectX.Direct3D;

namespace Kwadrat
{
	/// <summary>
	/// Summary description for ToolForm.
	/// </summary>
	public class ToolForm : System.Windows.Forms.Form
	{
		#region Interop Code 

		private const int SC_CLOSE=0xF060; 
		private const int MF_BYCOMMAND=0x0; 
		private const int MF_GRAYED=0x1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown numericUpDown4;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		public System.Windows.Forms.Label label13;
		public System.Windows.Forms.Label label14; 
		private const int MF_ENABLED=0x0; 

		[DllImport("user32.dll", SetLastError = true)] 
		private static extern int GetSystemMenu(IntPtr hWnd, int revert); 

		[DllImport("user32.dll", SetLastError = true)] 
		private static extern int EnableMenuItem(int menu, int ideEnableItem, int enable); 

		public static void Disable(Form form) 
		{ 
			IntPtr hWnd = form.Handle; 
			int SystemMenu = GetSystemMenu(hWnd,0); 
			int PreviousState = EnableMenuItem(SystemMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED); 
			if(PreviousState == -1) 
				throw new Exception("The close menu does not exist"); 
		}

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private MainForm xxx;

		public ToolForm(MainForm parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			xxx=parent;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.textBox1.Location = new System.Drawing.Point(136, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(72, 22);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "1";
			// 
			// textBox2
			// 
			this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.textBox2.Location = new System.Drawing.Point(136, 48);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(72, 22);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "1";
			// 
			// textBox3
			// 
			this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.textBox3.Location = new System.Drawing.Point(136, 80);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(72, 22);
			this.textBox3.TabIndex = 2;
			this.textBox3.Text = "1";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Szerokoœæ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "d³ugoœæ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Wysokoœæ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Start";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(112, 24);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(72, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "Pauza";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.DecimalPlaces = 2;
			this.numericUpDown1.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 131072});
			this.numericUpDown1.Location = new System.Drawing.Point(136, 120);
			this.numericUpDown1.Maximum = new System.Decimal(new int[] {
																		   180,
																		   0,
																		   0,
																		   0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(80, 22);
			this.numericUpDown1.TabIndex = 9;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.DecimalPlaces = 2;
			this.numericUpDown2.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 131072});
			this.numericUpDown2.Location = new System.Drawing.Point(136, 152);
			this.numericUpDown2.Maximum = new System.Decimal(new int[] {
																		   90,
																		   0,
																		   0,
																		   0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(80, 22);
			this.numericUpDown2.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 23);
			this.label4.TabIndex = 11;
			this.label4.Text = "K¹t Odchylenia";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 24);
			this.label5.TabIndex = 12;
			this.label5.Text = "Prêdkoœæ K¹towa";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 464);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(410, 88);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Animacja";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(16, 56);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(168, 23);
			this.button4.TabIndex = 9;
			this.button4.Text = "Reset";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(0, 296);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(400, 168);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Wartoœci";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(152, 88);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(168, 24);
			this.label14.TabIndex = 5;
			this.label14.Text = "0";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(152, 56);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128, 23);
			this.label13.TabIndex = 4;
			this.label13.Text = "0";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(40, 88);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 3;
			this.label12.Text = "suma Energii";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(136, 24);
			this.label11.TabIndex = 2;
			this.label11.Text = "Energia kinetyczna";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 32);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(128, 24);
			this.label7.TabIndex = 1;
			this.label7.Text = "Energia Potencjalna";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(152, 32);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "0";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(0, 256);
			this.trackBar1.Maximum = 1000;
			this.trackBar1.Minimum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(160, 40);
			this.trackBar1.TabIndex = 15;
			this.trackBar1.TickFrequency = 30;
			this.trackBar1.Value = 100;
			this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(168, 264);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 23);
			this.label8.TabIndex = 16;
			this.label8.Text = "100";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 184);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(112, 23);
			this.label9.TabIndex = 18;
			this.label9.Text = "Grawitacja";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.DecimalPlaces = 2;
			this.numericUpDown3.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 131072});
			this.numericUpDown3.Location = new System.Drawing.Point(136, 192);
			this.numericUpDown3.Maximum = new System.Decimal(new int[] {
																		   50,
																		   0,
																		   0,
																		   0});
			this.numericUpDown3.Minimum = new System.Decimal(new int[] {
																		   50,
																		   0,
																		   0,
																		   -2147483648});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(80, 22);
			this.numericUpDown3.TabIndex = 19;
			this.numericUpDown3.ThousandsSeparator = true;
			this.numericUpDown3.Value = new System.Decimal(new int[] {
																		 981,
																		 0,
																		 0,
																		 131072});
			this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 216);
			this.label10.Name = "label10";
			this.label10.TabIndex = 20;
			this.label10.Text = "delta";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.DecimalPlaces = 4;
			this.numericUpDown4.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 262144});
			this.numericUpDown4.Location = new System.Drawing.Point(136, 224);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(80, 22);
			this.numericUpDown4.TabIndex = 21;
			this.numericUpDown4.Value = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 131072});
			this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
			// 
			// ToolForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(410, 552);
			this.Controls.Add(this.numericUpDown4);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.numericUpDown3);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ToolForm";
			this.Text = "ToolForm";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ToolForm_Closing);
			this.Load += new System.EventHandler(this.ToolForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ToolForm_Load(object sender, System.EventArgs e)
		{
			ToolForm.Disable(this);
		}

		private void ToolForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			xxx.bDrawObj=true;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			xxx.bDrawObj=false;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			try
			{	
				float a=1,b=1,c=1, grav=9.81f;
				float rotVel=0,angle=0;

				a = (float)Double.Parse(textBox1.Text);
				b = (float)Double.Parse(textBox2.Text);
				c = (float)Double.Parse(textBox3.Text);
				grav = (float)Decimal.ToDouble(numericUpDown3.Value);

				rotVel = (float)Decimal.ToDouble(numericUpDown2.Value);
				angle = (float)Decimal.ToDouble(numericUpDown1.Value);

				// signal to stop drawing and processing
				xxx.bDrawObj = false;
				xxx.obj = null;
				xxx.obj = new BoxObject3D(xxx.device, a, b, c);
				xxx.obj.setGrav(grav);
				xxx.obj.Translate(-a/2f,-b/2f,-c/2f);
				xxx.obj.AxisRotate(xxx.obj.upperAxis, (float)(Math.PI * (float)angle/180f));
				xxx.obj.SetOmega(rotVel);
				xxx.obj.delta = (float)Decimal.ToDouble(numericUpDown4.Value);
			} 
			catch ( Exception ess )
			{
				MessageBox.Show(this, ess.Message);
			}
		}

		private void trackBar1_ValueChanged(object sender, System.EventArgs e)
		{
			xxx.obj.psize=trackBar1.Value;
			label8.Text = ""+trackBar1.Value;
		}

		private void numericUpDown3_ValueChanged(object sender, System.EventArgs e)
		{
			try 
			{
				float grav=9.81f;
				grav = (float)Decimal.ToDouble(numericUpDown3.Value);
				xxx.obj.setGrav(grav);
			} 
			catch (Exception r)
			{
				MessageBox.Show(this, r.Message);
			}
		}

		private void numericUpDown4_ValueChanged(object sender, System.EventArgs e)
		{
			try 
			{
				float grav=0.01f;
				grav = (float)Decimal.ToDouble(numericUpDown4.Value);
				xxx.obj.delta = grav;
			} 
			catch (Exception r)
			{
				MessageBox.Show(this, r.Message);
			}
		
		}

	}
}
