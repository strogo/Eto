using System;

namespace Eto.Forms
{
	public interface IRadioButton : ITextControl
	{
		void Create (RadioButton controller);

		bool Checked { get; set; }
	}

	public class RadioButton : TextControl
	{
		public event EventHandler<EventArgs> CheckedChanged;
		public event EventHandler<EventArgs> Click;

		new IRadioButton Handler { get { return (IRadioButton)base.Handler; } }

		public void OnClick (EventArgs e)
		{
			if (Click != null)
				Click (this, e);
		}
		
		public void OnCheckedChanged (EventArgs e)
		{
			if (CheckedChanged != null)
				CheckedChanged (this, e);
		}

		public RadioButton()
			: this(null, null)
		{
		}
		
		public RadioButton (RadioButton controller = null, Generator generator = null)
			: this(generator, typeof(IRadioButton), controller)
		{
		}
		
		protected RadioButton (Generator generator, Type type, RadioButton controller, bool initialize = true)
			: base (generator, type, false)
		{
			Handler.Create (controller);
			if (initialize)
				Initialize ();
		}

		public virtual bool Checked {
			get { return Handler.Checked; }
			set { Handler.Checked = value; }
		}

	}
}
