using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Styling;

//using Tsinswreng.AvlnTools.Tools;
using Tsinswreng.CsCore;

namespace Tsinswreng.Avln.Dsl;
using Controls = global::Avalonia.Controls.Controls;
using NonGenericList = System.Collections.IList;
public static class Extn{
	extension<T>(ref T z)
		where T:struct
	{
		public void Set(T o){
			z = o;
		}
	}
	
	[Doc(@$"
	var t = new TextBlock();
	t.Prop(x=>x.Text) -> TextBlock.TextProperty
	")]
	public static AvaloniaProperty Prop<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T
	>(
		this T z, Expression<Func<T, object?>> PropertySelector
	)where T:AvaloniaObject
	{
		var Expr = UnwrapPropertySelector(PropertySelector.Body);
		if(Expr is not MemberExpression m
			|| m.Member is not PropertyInfo p
		){
			throw new ArgumentException("PropertySelector must be a property selector expression.");
		}
		var StaticName = p.Name+"Property";
		FieldInfo? AvlnPropField = null;
		for(var CurType = typeof(T); CurType is not null; CurType = CurType.BaseType){
			AvlnPropField = CurType.GetField(
				StaticName,
				BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy
			);
			if(AvlnPropField is not null){
				break;
			}
		}
		if(AvlnPropField is null){
			throw new InvalidOperationException(
				$"Avalonia property field '{StaticName}' was not found on type '{typeof(T)}' or its base types."
			);
		}
		return (AvaloniaProperty)AvlnPropField.GetValue(null)!;
	}

	/// `Expression<Func<T, object?>>` 在值類型/可空值類型屬性上常帶一層裝箱轉換。
	/// 這裏先剝掉外層轉換，再按成員訪問解析 AvaloniaProperty。
	static Expression UnwrapPropertySelector(Expression Expr){
		while(Expr is UnaryExpression u
			&& (
				u.NodeType == ExpressionType.Convert
				|| u.NodeType == ExpressionType.ConvertChecked
				|| u.NodeType == ExpressionType.TypeAs
			)
		){
			Expr = u.Operand;
		}
		return Expr;
	}
	
	public static BindingExpressionBase CBind<TTar>
	(
		this AvaloniaObject z, AvaloniaProperty AvlnProp
		,Expression<Func<TTar, object?>> TargetPropSlctr
		//do not rename the following param
		//keep them the same as the prop of Binding
		,BindingMode Mode = default
		,IValueConverter? Converter = default
		,object? ConverterParameter = default
		,CompiledBindingPath? Path = default
		,object? Source = default
		,Type? DataType = default
	){
		return z.Bind(AvlnProp, CBE.Mk(
			TargetPropSlctr, Mode, Converter, ConverterParameter, Path, Source, DataType
		));
	}
	

	//下ʹ方法 須 手動傳兩泛型參數、不便也
	// public static BindingExpressionBase CBind<TCtrl, TTar>
	// (
	// 	this TCtrl z, Func<TCtrl, AvaloniaProperty> AvlnPropScltr
	// 	,Expression<Func<TTar, object?>> TargetPropSlctr
	// 	,BindingMode Mode = default
	// 	,IValueConverter? Converter = default
	// 	,object? ConverterParameter = default
	// 	,CompiledBindingPath? Path = default
	// 	,object? Source = default
	// 	,Type? DataType = default
	// )where TCtrl:Control
	// {
	// 	var AvlnProp = AvlnPropScltr(z);
	// 	return z.Bind(AvlnProp, CBE.Mk(
	// 		TargetPropSlctr, Mode, Converter, ConverterParameter, Path, Source, DataType
	// 	));
	// }


	public static NonGenericList A<TItem>(
		this NonGenericList z
		,TItem Child
		,Action<TItem>? FnInit = null
	){
		z.Add(Child!);
		FnInit?.Invoke(Child);
		return z;
	}

	public static Style AddTo(
		this Style z
		,Styles Styles
		,Action<Style>? FnInit = null
	){
		FnInit?.Invoke(z);
		Styles.Add(z);
		return z;
	}

	public static ICollection<IStyle> A<TItem>(
		this ICollection<IStyle> z
		,TItem Child
		,Action<TItem>? FnInit = null
	)where TItem:IStyle{
		z.Add(Child!);
		FnInit?.Invoke(Child);
		return z;
	}
	
	public static ICollection<Control> A<TItem>(
		this ICollection<Control> z
		,TItem Child
		,Action<TItem>? FnInit = null
	)where TItem:Control{
		z.Add(Child!);
		FnInit?.Invoke(Child);
		return z;
	}
	
	[Doc(@$"for .Children.A()")]
	public static Controls A<TChild>(
		this Controls z
		,TChild Child
		,Action<TChild>? FnInit = null
	)where TChild: Control{
		z.Add(Child);
		FnInit?.Invoke(Child);
		return z;
	}

	public static Panel A<TChild>(
		this Panel z
		,TChild Child
		,Action<TChild>? FnInit = null
	)where TChild:Control{
		z.Children.A(Child,FnInit);
		return z;
	}

	public static TControl SetContent<TControl>(
		this ContentControl ContentControl
		,TControl ControlAsContent
		,Action<TControl>? FnInit = null
	){
		ContentControl.Content = ControlAsContent;
		FnInit?.Invoke(ControlAsContent);
		return ControlAsContent;
	}

	public static TControl SetChild<TControl>(
		this Decorator ContentControl
		,TControl ControlAsContent
		,Action<TControl>? FnInit = null
	)where TControl:Control{
		ContentControl.Child = ControlAsContent;
		FnInit?.Invoke(ControlAsContent);
		return ControlAsContent;
	}
	
	public class ClsHorizontalAlignment{
		public static ClsHorizontalAlignment Inst = new();
		public HorizontalAlignment Stretch=>HorizontalAlignment.Stretch;
		public HorizontalAlignment Left=>HorizontalAlignment.Left;
		public HorizontalAlignment Center=>HorizontalAlignment.Center;
		public HorizontalAlignment Right=>HorizontalAlignment.Right;
	}
	
	public static TSelf HAlign<TSelf>(
		this TSelf z
		,HorizontalAlignment v
	)
		where TSelf:Layoutable
	{
		z.HorizontalAlignment = v;
		return z;
	}

	public static TSelf HAlign<TSelf>(
		this TSelf z
		,Func<ClsHorizontalAlignment, HorizontalAlignment> Fn
	)
		where TSelf:Layoutable
	{
		z.HorizontalAlignment = Fn(ClsHorizontalAlignment.Inst);
		return z;
	}
	
	public static TSelf HCAlign<TSelf>(
		this TSelf z
		,HorizontalAlignment v
	)
		where TSelf:ContentControl
	{
		z.HorizontalContentAlignment = v;
		return z;
	}

	public static TSelf HCAlign<TSelf>(
		this TSelf z
		,Func<ClsHorizontalAlignment, HorizontalAlignment> Fn
	)
		where TSelf:ContentControl
	{
		z.HorizontalContentAlignment = Fn(ClsHorizontalAlignment.Inst);
		return z;
	}
	

	public class ClsVerticalAlignment{
		public static ClsVerticalAlignment Inst = new();
		public VerticalAlignment Stretch=>VerticalAlignment.Stretch;
		public VerticalAlignment Top=>VerticalAlignment.Top;
		public VerticalAlignment Center=>VerticalAlignment.Center;
		public VerticalAlignment Bottom=>VerticalAlignment.Bottom;
	}
	public static TSelf VAlign<TSelf>(
		this TSelf z
		,VerticalAlignment v
	)
		where TSelf:Layoutable
	{
		z.VerticalAlignment = v;
		return z;
	}
	
	public static TSelf VAlign<TSelf>(
		this TSelf z
		,Func<ClsVerticalAlignment, VerticalAlignment> Fn
	)
		where TSelf:Layoutable
	{
		z.VerticalAlignment = Fn(ClsVerticalAlignment.Inst);
		return z;
	}
	
	public static TSelf VCAlign<TSelf>(
		this TSelf z
		,VerticalAlignment v
	)
		where TSelf:ContentControl
	{
		z.VerticalContentAlignment = v;
		return z;
	}
	
	public static TSelf VCAlign<TSelf>(
		this TSelf z
		,Func<ClsVerticalAlignment, VerticalAlignment> Fn
	)
		where TSelf:ContentControl
	{
		z.VerticalContentAlignment = Fn(ClsVerticalAlignment.Inst);
		return z;
	}

	public static Style Set(
		this Style z, AvaloniaProperty property, object? value
	){
		z.Setters.Add(new Setter(property, value));

		return z;
	}

	extension<TCtrl>(TCtrl z)
		where TCtrl:Control
	{
		public Action<TCtrl> Init{
			set{value(z);}
		}

		public (AvaloniaProperty property, IBinding binding) Bind{
			set{
				z.Bind(value.property,value.binding);
			}
		}
		// public HorizontalAlignment HAlign{
		// 	get=>z.HorizontalAlignment;
		// 	set=>z.HorizontalAlignment = value;
		// }
		// public VerticalAlignment VAlign{
		// 	get=>z.VerticalAlignment;
		// 	set=>z.VerticalAlignment = value;
		// }
	}
	
	extension<TSelf>(TSelf z)
		where TSelf : Grid
	{
		public TSelf SetRowDefs(
			params IEnumerable<RowDefinition> RowDefs
		){
			z.RowDefinitions = [..RowDefs];
			return z;
		}

		public TSelf SetColDefs(
			params IEnumerable<ColumnDefinition> ColDefs
		){
			z.ColumnDefinitions= [..ColDefs];
			return z;
		}
	}

}

