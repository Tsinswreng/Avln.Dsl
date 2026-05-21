using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
namespace Tsinswreng.Avln.Dsl;
public partial interface IViewModel{}

public static class ExtnIViewModel{
	extension<TSelf>(TSelf? z)
		//where TSelf : IViewModel
	{
		public BindingExpressionBase Bind(
			AvaloniaObject C
			,AvaloniaProperty AvlnProp
			,Expression<Func<TSelf, object?>> TargetPropSlctr
			//do not rename the following param
			//keep them the same as the prop of Binding
			,BindingMode Mode = default
			,IValueConverter? Converter = default
			,object? ConverterParameter = default
			,CompiledBindingPath? Path = default
			,object? Source = default
			,Type? DataType = default
		){
			return C.CBind(
				AvlnProp, TargetPropSlctr, Mode, Converter, ConverterParameter, Path, Source, DataType
			);
		}
		
		public BindingExpressionBase Bind<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
			TCtrl
		>(
			TCtrl C
			,Expression<Func<TCtrl, object?>> AvlnPropSlctr
			,Expression<Func<TSelf, object?>> TargetPropSlctr
			//do not rename the following param
			//keep them the same as the prop of Binding
			,BindingMode Mode = default
			,IValueConverter? Converter = default
			,object? ConverterParameter = default
			,CompiledBindingPath? Path = default
			,object? Source = default
			,Type? DataType = default
		)where TCtrl:AvaloniaObject
		{
			var Prop = Extn.Prop(C, AvlnPropSlctr);
			return C.CBind(
				Prop, TargetPropSlctr, Mode, Converter, ConverterParameter, Path, Source, DataType
			);
		}
	}
}
