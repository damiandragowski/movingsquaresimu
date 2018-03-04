using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
namespace Kwadrat
{
	/// <summary>
	/// Summary description for QPTimer.
	/// </summary>
	public enum TimerState{ Stopped, Running}

	public class QPTimer
	{
		[System.Security.SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);
		[System.Security.SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceCounter(ref long PerformanceCount);

		private long tickFreq;
		private TimerState timerState;
		private long lastTickCount;

		public QPTimer()
		{
			this.timerState = TimerState.Stopped;

			if(QueryPerformanceFrequency(ref tickFreq) == false)
			{
				throw new ApplicationException("Failed to query for the performance frequency!");
			}
		}

		public void Start()
		{
			this.timerState = TimerState.Stopped;
			this.lastTickCount = GetCurrentCount();
			this.timerState = TimerState.Running;
		}

		public void Stop()
		{
			this.timerState = TimerState.Stopped;
		}

		public double GetElapsedTime()
		{
			if(this.timerState == TimerState.Stopped)
			{
				throw new ApplicationException("Timer is not running!");
			}

			long currTick = GetCurrentCount();
			double elapsed = (currTick-this.lastTickCount)/(double)this.tickFreq;
			this.lastTickCount = currTick;
			return elapsed;
		}

		public TimerState State
		{
			get
			{
				return this.timerState;
			}
		}

		protected long GetCurrentCount()
		{
			long tickCount = 0;
			if(QueryPerformanceCounter(ref tickCount) == false)
			{
				throw new ApplicationException("Failed to query performance counter!");
			}
			return tickCount;
		}
	}
}
