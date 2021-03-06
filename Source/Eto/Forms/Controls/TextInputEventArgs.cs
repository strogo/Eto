using System;

namespace Eto.Forms
{
	public class TextInputEventArgs : EventArgs
	{
		/// <summary>
		/// The entered text, or null if no text was entered.
		/// </summary>
		public string Text { get; set; }
		public bool DeleteBackwards { get; set; }
	}
}
