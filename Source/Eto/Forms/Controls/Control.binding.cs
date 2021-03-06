using System;

namespace Eto.Forms
{
	public partial class Control
	{
		/// <summary>
		/// Adds a new dual binding between the control and the specified object
		/// </summary>
		/// <param name="propertyName">Property on the control to update</param>
		/// <param name="source">Object to bind to</param>
		/// <param name="sourcePropertyName">Property on the source object to retrieve/set the value of</param>
		/// <param name="mode">Mode of the binding</param>
		/// <returns>A new instance of the DualBinding class that is used to control the binding</returns>
		public DualBinding Bind(string propertyName, object source, string sourcePropertyName, DualBindingMode mode = DualBindingMode.TwoWay)
		{
			var binding = new DualBinding(
				source,
				sourcePropertyName,
				this,
				propertyName,
				mode
			);
			Bindings.Add(binding);
			return binding;
		}

		/// <summary>
		/// Adds a new dual binding between the control and the specified source binding
		/// </summary>
		/// <param name="widgetPropertyName">Property on the control to update</param>
		/// <param name="sourceBinding">Binding to get/set the value to from the control</param>
		/// <param name="mode">Mode of the binding</param>
		/// <returns>A new instance of the DualBinding class that is used to control the binding</returns>
		public DualBinding Bind(string widgetPropertyName, DirectBinding sourceBinding, DualBindingMode mode = DualBindingMode.TwoWay)
		{
			var binding = new DualBinding(
				sourceBinding,
				new ObjectBinding(this, widgetPropertyName),
				mode
			);
			Bindings.Add(binding);
			return binding;
		}

		/// <summary>
		/// Adds a new binding with the control and the the control's current data context 
		/// </summary>
		/// <remarks>
		/// This binds to a property of the <see cref="Control.DataContext"/>, which will return the topmost value
		/// up the control hierarchy.  For example, you can set the DataContext of your form or panel, and then bind to properties
		/// of that context on any of the child controls such as a text box, etc.
		/// </remarks>
		/// <param name="controlPropertyName">Property on the control to update</param>
		/// <param name="dataContextPropertyName">Property on the control's <see cref="Control.DataContext"/> to bind to the control</param>
		/// <param name="mode">Mode of the binding</param>
		/// <param name="defaultControlValue">Default value to set to the control when the value from the DataContext is null</param>
		/// <param name="defaultContextValue">Default value to set to the DataContext property when the control value is null</param>
		/// <returns>A new instance of the DualBinding class that is used to control the binding</returns>
		public DualBinding Bind(string controlPropertyName, string dataContextPropertyName, DualBindingMode mode = DualBindingMode.TwoWay, object defaultControlValue = null, object defaultContextValue = null)
		{
			var dataContextBinding = new PropertyBinding(dataContextPropertyName);
			var controlBinding = new PropertyBinding(controlPropertyName);
			return Bind(controlBinding, dataContextBinding, mode, defaultControlValue, defaultContextValue);
		}

		public DualBinding Bind(IndirectBinding controlBinding, DirectBinding valueBinding, DualBindingMode mode = DualBindingMode.TwoWay)
		{
			var binding = new ObjectBinding(this, controlBinding);
			return binding.Bind(valueBinding: valueBinding, mode: mode);
		}

		public DualBinding Bind(IndirectBinding controlBinding, object objectValue, IndirectBinding objectBinding, DualBindingMode mode = DualBindingMode.TwoWay, object defaultControlValue = null, object defaultContextValue = null)
		{
			var valueBinding = new ObjectBinding(objectValue, objectBinding) {
				SettingNullValue = defaultContextValue,
				GettingNullValue = defaultControlValue
			};
			return Bind(controlBinding, valueBinding, mode);
		}

		public DualBinding Bind(IndirectBinding controlBinding, IndirectBinding dataContextBinding, DualBindingMode mode = DualBindingMode.TwoWay, object defaultControlValue = null, object defaultContextValue = null)
		{
			var binding = new ObjectBinding(this, controlBinding);
			return binding.Bind(dataContextBinding, mode, defaultControlValue, defaultContextValue);
		}
	}
}

