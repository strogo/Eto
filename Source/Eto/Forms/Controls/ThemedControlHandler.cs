using System;
using Eto.Drawing;

namespace Eto.Forms
{
	/// <summary>
	/// Base class for implementing Eto controls
	/// using other Eto controls.
	/// This allows a single implementation to 
	/// be used in multiple platforms and is
	/// useful in a couple of scenarios:
	/// 
	/// a) Creating default implementations of a control
	/// on platforms that do not support the control
	/// natively.
	///    
	/// b) Implementing a control with a non-native
	/// look and feel that is consistent across
	/// platforms.
	/// <typeparam name="TControl">The Eto control used to create the custom implementation, e.g. Panel</typeparam>
	/// <typeparam name="TWidget">The control being implemented, eg TabControl</typeparam>
	/// </summary>
	public class ThemedControlHandler<TControl, TWidget> : WidgetHandler<TControl, TWidget>, IControl
		where TControl : Control
		where TWidget : Control
	{
		public Color BackgroundColor
		{
			get { return Control.BackgroundColor; }
			set { Control.BackgroundColor = value; }
		}

		public virtual bool PropagateLoadEvents { get { return true; } }

		public Size Size
		{
			get { return Control.Size; }
			set { Control.Size = value; }
		}

		public virtual bool Enabled
		{ 
			get { return Control.Enabled; } 
			set { Control.Enabled = value; }
		}

		public virtual void Invalidate()
		{
			Control.Invalidate();
		}

		public virtual void Invalidate(Rectangle rect)
		{
			Control.Invalidate(rect);
		}

		public virtual void SuspendLayout()
		{
			Control.SuspendLayout();
		}

		public virtual void ResumeLayout()
		{
			Control.ResumeLayout();
		}

		public virtual void Focus()
		{
			Control.Focus();
		}

		public virtual bool HasFocus
		{
			get { return Control.HasFocus; }
		}

		public virtual bool Visible
		{
			get { return Control.Visible; }
			set { Control.Visible = value; }
		}

		public virtual void OnPreLoad(EventArgs e)
		{
			if (PropagateLoadEvents)
				Control.OnPreLoad(e);
		}

		public virtual void OnLoad(EventArgs e)
		{
			if (PropagateLoadEvents)
				Control.OnLoad(e);
		}

		public virtual void OnLoadComplete(EventArgs e)
		{
			if (PropagateLoadEvents)
				Control.OnLoadComplete(e);
		}

		public virtual void OnUnLoad(EventArgs e)
		{
			if (PropagateLoadEvents)
				Control.OnUnLoad(e);
		}

		public virtual void SetParent(Container parent)
		{
			Control.Parent = parent;
		}

		public virtual PointF PointFromScreen(PointF point)
		{
			return Control.PointFromScreen(point);
		}

		public virtual PointF PointToScreen(PointF point)
		{
			return Control.PointToScreen(point);
		}

		public virtual void MapPlatformCommand(string systemAction, Command action)
		{
			Control.MapPlatformCommand(systemAction, action);
		}

		public virtual Point Location
		{
			get { return Control.Location; }
		}
		#if DESKTOP
		public virtual string ToolTip
		{
			get { return Control.ToolTip; }
			set { Control.ToolTip = value; }
		}

		public virtual Cursor Cursor
		{
			get { return Control.Cursor; }
			set { Control.Cursor = value; }
		}
		#endif
		public virtual object ControlObject
		{
			get { return Control; }
		}

		#region Events

		public override void AttachEvent(string id)
		{
			var handled = false;

			switch (id)
			{
				case Eto.Forms.Control.KeyDownEvent:
					Control.KeyDown += (s, e) => Widget.OnKeyDown(e);
					handled = true;
					break;
				case Eto.Forms.Control.KeyUpEvent:
					Control.KeyUp += (s, e) => Widget.OnKeyUp(e);
					handled = true;
					break;
				case Eto.Forms.Control.SizeChangedEvent:
					Control.SizeChanged += (s, e) => Widget.OnSizeChanged(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseDoubleClickEvent:
					Control.MouseDoubleClick += (s, e) => Widget.OnMouseDoubleClick(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseEnterEvent:
					Control.MouseEnter += (s, e) => Widget.OnMouseEnter(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseLeaveEvent:
					Control.MouseLeave += (s, e) => Widget.OnMouseLeave(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseDownEvent:
					Control.MouseDown += (s, e) => Widget.OnMouseDown(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseUpEvent:
					Control.MouseUp += (s, e) => Widget.OnMouseUp(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseMoveEvent:
					Control.MouseMove += (s, e) => Widget.OnMouseMove(e);
					handled = true;
					break;
				case Eto.Forms.Control.MouseWheelEvent:
					Control.MouseWheel += (s, e) => Widget.OnMouseWheel(e);
					handled = true;
					break;
				case Eto.Forms.Control.GotFocusEvent:
					Control.GotFocus += (s, e) => Widget.OnGotFocus(e);
					handled = true;
					break;
				case Eto.Forms.Control.LostFocusEvent:
					Control.LostFocus += (s, e) => Widget.OnLostFocus(e);
					handled = true;
					break;
#if TODO
				case Eto.Forms.DragDropInputSource.DragDropEvent:
					Control.DragDrop += (s, e) =>
						handleDragEvent(
							Widget.DragDropInputSource.OnDragDrop,
							e);
					handled = true;
					break;
				case Eto.Forms.DragDropInputSource.DragEnterEvent:
					Control.DragEnter += (s, e) =>
						handleDragEvent(
							Widget.DragDropInputSource.OnDragEnter,
							e);
					handled = true;
					break;
				case Eto.Forms.DragDropInputSource.DragOverEvent:
					Control.DragOver += (s, e) =>
						handleDragEvent(
							Widget.DragDropInputSource.OnDragOver,
							e);
					handled = true;
					break;
				case Eto.Forms.DragDropInputSource.GiveFeedbackEvent:
					Control.GiveFeedback += (s, e) =>
						Widget.DragDropInputSource.OnGiveFeedback(
							e.ToEto());
					handled = true;
					break;
				case Eto.Forms.DragDropInputSource.QueryContinueDragEvent:
					Control.QueryContinueDrag += (s, e) =>
						// TODO: convert the result back to SWF
						Widget.DragDropInputSource.OnQueryContinueDrag(
							e.ToEto());
					handled = true;
					break;
#endif
			}

			if (!handled)
				base.AttachEvent(id);
		}

		#endregion

	}
}
