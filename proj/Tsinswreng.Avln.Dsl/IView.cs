using System.Linq.Expressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
namespace Tsinswreng.Avln.Dsl;

public interface IView<TCtx>{
	public TCtx? Ctx{get;set;}
}

public static class ExtnIView{
	extension<TCtx>(IView<TCtx> z){
		public BindingExpressionBase Bind(
			Control C
			,AvaloniaProperty AvlnProp
			,Expression<Func<TCtx, object?>> TargetPropSlctr
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
	}
}
