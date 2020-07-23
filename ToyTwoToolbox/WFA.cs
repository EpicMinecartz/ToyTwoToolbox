using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	/// <summary>Standalone objects for storing information about WinForms Animations with timers <para />
	/// Note: Set your <see cref="Timer.Interval"/> to <c>1</c> and set the <see cref="UIA.CycleOffset"/> and <see cref="UIA.CycleTime"/> accordingly</summary>
	public class UIA {
		/// <summary><see cref="Int32"/> holding the current time of the animation. Try to use Milliseconds</summary>
		public int CycleOffset = new int();
		/// <summary><see cref="Int32"/> holding the total time of the animation. Try to use Milliseconds</summary>
		public int CycleTime = new int();
		private int _CycleTime = new int();
		private bool _Reversed = false;
		/// <summary>The <see cref="Timer"/> that is controlled by this animation object. [Optional]</summary>
		public Timer Timer_Handle;
		/// <summary>Whether to stop the associated timer or not when <see cref="UIA.CycleOffset"/> equals <see cref="UIA.CycleTime"/>. <para />
		/// Requires <see cref="Timer_Handle"/> to be a valid <see cref="Timer"/> handle.</summary>
		public bool Timer_AutoStop = true;
		/// <summary>Whether to reverse the animation or not when <see cref="UIA.CycleOffset"/> equals <see cref="UIA.CycleTime"/>. <para />
		/// Requires <see cref="Timer_Handle"/> to be a valid <see cref="Timer"/> handle.</summary>
		public bool Timer_ReverseAnimWhenMaximum = false;
		/// <summary>Whether to delete the timer when <see cref="UIA.CycleOffset"/> equals <see cref="UIA.CycleTime"/>. <para />
		/// Requires <see cref="Timer_Handle"/> to be a valid <see cref="Timer"/> handle. <para />
		/// [TOP LEVEL] Warning, this will override <see cref="UIA.Timer_ReverseAnimWhenMaximum"/></summary>
		public bool Timer_DestroyOnEnd = false;

		public bool Reversed {
			get {
				return _Reversed;
			}
			private set {
				_Reversed = value;
			}
		}

		public int State = new int();
		public List<UIAS> States = new List<UIAS>();

		/// <summary>
		/// Initilizes a new <see cref="UIA"/> object for storing WinForms animation data
		/// </summary>
		/// <param name="CycleOffset"></param>
		/// <param name="CycleTime"></param>
		/// <param name="_CycleTime"></param>
		/// <param name="_Reversed"></param>
		/// <param name="Timer_Handle"></param>
		/// <param name="Timer_AutoStop"></param>
		/// <param name="Timer_ReverseAnimWhenMaximum"></param>
		public UIA(int CycleOffset = 0, int CycleTime = 0, int _CycleTime = 0, bool _Reversed = false, Timer Timer_Handle = null, bool Timer_AutoStop = true, bool Timer_ReverseAnimWhenMaximum = false) { //backup purposes
			_CycleTime = CycleTime;
		}

		/// <summary>Reverses the current animation.</summary>
		/// <returns>The <see cref="UIA.Reversed"/> flag.</returns>
		public bool ReverseAnimation(bool AutoStartTimer = false) {
			if (Reversed == false) { //we arent reversed yet
									 //the problem is any value < 100 is normal, while > 100 is reversed
									 //thus setting reversed=true while offset = 50 will mean we have to go to 100, then to 200
									 //so our animating item will go forward 50%, then back 100%
									 //not good, so we need to check if we are =, if so then shift Time forward
									 //if not =, then we multiply both, like this
									 //If CycleOffset <> CycleTime Then
									 //    CycleOffset *= 2
									 //End If
									 //CycleTime *= 2
			} else { //we are reversed, revert original state
					 //on the way back its a bit more complicated
					 //we gotta make sure we dont ?/0 at any time, obv lol
					 //in this scenario, cycle time is 200, and cycle offset can be =, or anything below, fuck
					 //luckily, 200 = 0 (in cycletime sense), so we do a similar check to above
					 //if cycleoffset = cycletime then set cycletime to 100 and cycleoffset to 0
					 //otherwise just divide by two as we are mid anim, and 50 = 150 (i have 800 iq, dont @ me)
				if (CycleOffset != CycleTime * 2) {
					CycleOffset /= 2;
				} else {
					CycleOffset = 0;
				}
				//CycleTime /= 2
			}
			Reversed = !Reversed;
			if (AutoStartTimer == true) { Timer_Handle.Start(); }
			return Reversed;
		}

		/// <summary>Increments <see cref="UIA.CycleOffset"/> by a spesific amount. 1 by default. <para/>
		/// If <see cref="UIA.Timer_AutoStop"/> or <see cref="UIA.Timer_ReverseAnimWhenMaximum"/> are set, then action is taken based around them too.</summary>
		/// <param name="Value">How much to increment <see cref="UIA.CycleOffset"/> by.</param>
		/// <returns>Boolean representing whether <see cref="UIA.CycleOffset"/> equals <see cref="UIA.CycleTime"/>.</returns>
		public bool Increment(int Value = 1) {
			//if we have access to the parent timer, then we can auto stop the timer if we reach maxium, or flip and stop
			if (CycleOffset >= ((Reversed == true) ? CycleTime * 2 : CycleTime)) { //the maximum has been reached
				if (Timer_Handle != null) { //we have access to the timer...
					if (Timer_DestroyOnEnd == true) {
						Timer_Handle.Dispose();
					}
					if (Timer_ReverseAnimWhenMaximum == true) { //when CycleOffset = CycleTime reverse animation?
						ReverseAnimation();
					}
					if (Timer_AutoStop == true) { //when CycleOffset = CycleTime should we stop the timer automatically?
						Timer_Handle.Stop();
					}
				}
			} else { //we still got a bit more to go
				CycleOffset += Value;
			}
			return (CycleOffset >= CycleTime);
		}
	} //Custom WinForms Animation Object

	public class UIAS {
		public int Position0 = new int();
		public int Position1 = new int();
		public int Correction = 0;
		
		public UIAS() { }

		public UIAS(int position0, int position1) {
			if (position0 < 0 || position1 < 0) {
				Correction = Math.Abs(0-Math.Min(position0, position1));
				position0 += Correction;
				position1 += Correction;
            }
			Position0 = position0;
			Position1 = position1;
        }
	}

}
