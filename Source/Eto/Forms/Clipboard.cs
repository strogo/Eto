using System;
using Eto;
using Eto.Drawing;
using System.IO;

namespace Eto.Forms
{
	public interface IClipboard : IInstanceWidget
	{
		string[] Types { get; }
		
		void SetString (string value, string type);

		void SetData (byte[] value, string type);
		
		string GetString (string type);

		byte[] GetData (string type);

		string Text { get; set; }

		string Html { get; set; }

		Image Image { get; set; }
		
		void Clear ();
	}
	
	public class Clipboard : InstanceWidget
	{
		new IClipboard Handler { get { return (IClipboard)base.Handler; } }
		
		public Clipboard()
			: this((Generator)null)
		{
		}

		public Clipboard (Generator generator)
			: base(generator, typeof(IClipboard))
		{
		}
		
		public string[] Types {
			get { return Handler.Types; }
		}
		
		public void SetDataStream (Stream stream, string type)
		{
			var buffer = new byte[stream.Length];
			if (stream.CanSeek && stream.Position != 0)
				stream.Seek (0, SeekOrigin.Begin);
			stream.Read (buffer, 0, buffer.Length);
			SetData (buffer, type);
		}
		
		public void SetData (byte[] value, string type)
		{
			Handler.SetData (value, type);
		}
		
		public byte[] GetData (string type)
		{
			return Handler.GetData (type);
		}
		
		public Stream GetDataStream (string type)
		{
			var buffer = GetData (type);
			return buffer == null ? null : new MemoryStream(buffer, false);
		}
		
		public void SetString (string value, string type)
		{
			Handler.SetString (value, type);
		}
		
		public string GetString (string type)
		{
			return Handler.GetString (type);
		}
		
		public string Text {
			get { return Handler.Text; }
			set { Handler.Text = value ?? ""; } // null check for consistency across platforms (Winforms throws an exception)
		}
		
		public string Html {
			get { return Handler.Html; }
			set { Handler.Html = value ?? ""; } // null check for consistency across platforms (Winforms throws an exception)
		}
		
		public Image Image {
			get { return Handler.Image; }
			set { Handler.Image = value; }
		}
		
		public void Clear ()
		{
			Handler.Clear ();
		}
		
	}
}

