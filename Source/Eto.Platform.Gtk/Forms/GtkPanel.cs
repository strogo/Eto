using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.Platform.GtkSharp
{
	public abstract class GtkPanel<TControl, TWidget> : GtkContainer<TControl, TWidget>
		where TControl: Gtk.Widget
		where TWidget: Panel
	{
		readonly Gtk.Alignment alignment;
		Control content;

		public override Gtk.Widget ContainerContentControl
		{
			get { return Control; }
		}

		protected GtkPanel()
		{
			alignment = new Gtk.Alignment(0, 0, 1, 1);
			this.Padding = Panel.DefaultPadding;
		}

		protected override void Initialize()
		{
			base.Initialize();
			SetContainerContent(alignment);
		}

		bool loaded;

		public override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!loaded)
			{
#if GTK2
				ContainerContentControl.SizeRequested += delegate(object o, Gtk.SizeRequestedArgs args)
				{
					var alloc = args.Requisition;
					if (MinimumSize.Width > 0)
						alloc.Width = Math.Max(alloc.Width, MinimumSize.Width);
					if (MinimumSize.Height > 0)
						alloc.Height = Math.Max(alloc.Height, MinimumSize.Height);
					args.Requisition = alloc;
				};
#else
				if (MinimumSize != Size.Empty)
					ContainerContentControl.SetSizeRequest(MinimumSize.Width, MinimumSize.Height);
#endif
				loaded = true;
			}
		}

		ContextMenu contextMenu;
		public ContextMenu ContextMenu
		{
			get { return contextMenu; }
			set { contextMenu = value; } // TODO
		}

		public Size MinimumSize { get; set; }

		public Padding Padding
		{
			get
			{
				uint top, left, right, bottom;
				alignment.GetPadding(out top, out bottom, out left, out right);
				return new Padding((int)left, (int)top, (int)right, (int)bottom);
			}
			set
			{
				alignment.SetPadding((uint)value.Top, (uint)value.Bottom, (uint)value.Left, (uint)value.Right);
			}
		}

		public Control Content
		{
			get { return content; }
			set
			{
				if (content != value)
				{
					foreach (var child in alignment.Children)
						alignment.Remove(child);
					content = value;
					var widget = content.GetContainerWidget();
					if (widget != null)
					{
						if (widget.Parent != null)
							((Gtk.Container)widget.Parent).Remove(widget);
						alignment.Child = widget;
						widget.ShowAll();
					}
				}
			}
		}

		protected abstract void SetContainerContent(Gtk.Widget content);
	}
}
