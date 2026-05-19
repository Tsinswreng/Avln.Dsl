//2025-03-09T21:11:06.192+08:00_W10-7
using System;
using System.Linq.Expressions;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Data.Core;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
namespace Tsinswreng.Avln.Dsl;


//于c#中用編譯期綁定
// usage:
// using Ctx = MyDataContext;
//,new Binding(nameof(ctx.hasValue)) 改成
//,CBE.Mk<Ctx>(x=>x.hasValue)
//RelativeBinging直用new Binging即可 不必用此
public partial class CBE : CompiledBindingExtension{
	public CBE(CompiledBindingPath path):base(path){}

	public static CompiledBindingPath Pth<T>(
		Expression<Func<T, object?>> propertySelector
	){
		return Pth<T, object?>(propertySelector);
	}


/// 除首個參數外 禁止依賴參數定義順序㕥傳參 須用命名參數 如 Mk<Ctx>(x=>x.Foo, Mode:BindingMode.TwoWay ...)
	public static CompiledBindingExtension Mk<T>(
		Expression<Func<T, object?>> PropertySelector
		//do not rename the following param
		//keep them the same as the prop of Binding
		,BindingMode Mode = default
		,IValueConverter? Converter = default
		,object? ConverterParameter = default
		,CompiledBindingPath? Path = default
		,object? Source = default
		,Type? DataType = default
	){
		var r = new CBE(Pth<T, object?>(PropertySelector)){};
		r.Mode = Mode;
		r.Converter = Converter;
		r.ConverterParameter = ConverterParameter;
		r.DataType = DataType;
		if(Path is not null){r.Path = Path;}
		if(Source is not null ){r.Source = Source;}
		return r;
	}

// 從表達式樹構建編譯綁定路徑，支持屬性訪問（如 x=>x.Property）和直接對象綁定（如 x=>x）
// 泛型參數 T 表示數據上下文類型，Tar 表示表達式返回值類型
public static CompiledBindingPath Pth<T, Tar>(
		Expression<Func<T, Tar>> propertySelector
	){
		var builder = new CompiledBindingPathBuilder();
		var body = propertySelector.Body;

		// 處理類型轉換表達式（如值類型裝箱）
		if (body is UnaryExpression { NodeType: ExpressionType.Convert } unaryExpr){
			body = unaryExpr.Operand;
		}

		switch (body){
			case MemberExpression memberExpr:  // 屬性訪問模式
				ProcessMemberExpression<T>(builder, memberExpr);
				break;
			case ParameterExpression paramExpr:  // 直接對象綁定模式
				ValidateObjectBinding(typeof(T), typeof(Tar));
				break;
			default:
				throw new ArgumentException("The expression must be a property access or object binding.");
		}

		return builder.Build();
	}

// 驗證來源類型是否可賦值給目標類型
// 用於直接對象綁定時的類型相容性檢查
private static void ValidateObjectBinding(Type sourceType, Type targetType){
	if (!targetType.IsAssignableFrom(sourceType)){
		//throw new InvalidOperationException($"类型不兼容：{sourceType}无法转换为{targetType}");
		throw new InvalidOperationException($"Type mismatch: {sourceType} cannot be assigned to {targetType}");
	}
}

// 處理成員表達式（屬性訪問），將屬性添加到編譯綁定路徑構建器中
// 使用反射建立 ClrPropertyInfo 並添加到路徑
private static void ProcessMemberExpression<T>(
	CompiledBindingPathBuilder builder
	,MemberExpression expr
){
	var propName = expr.Member.Name;
	var propType = expr.Type;

	var clrProp = new ClrPropertyInfo(
		propName,
		obj => ((T)obj).GetType().GetProperty(propName)?.GetValue(obj),
		(obj, val) => ((T)obj).GetType().GetProperty(propName)?.SetValue(obj, val),
		propType
	);

	builder.Property(clrProp, PropertyInfoAccessorFactory.CreateInpcPropertyAccessor);
}
}
