namespace Tsinswreng.AvlnDsl;

using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;


/// <typeparam name="TIn">對應ViewModel</typeparam>
/// <typeparam name="TRet">對應View</typeparam>
public partial class FnConvtr<TIn, TRet>
	:IValConvtrWithErr
{
	//第二個object?是Parameter
	public Func<TIn, object?, TRet>? FnConv{get;set;}
	//第二個object?是Parameter
	public Func<TRet, object?, TIn>? FnBack{get;set;}
	public Func<Exception, obj?>? OnErr{get;set;} = e => {
		Console.Error.WriteLine(e); return null;
	};
		public FnConvtr(Func<TIn, object?, TRet> FnConv){
			this.FnConv = FnConv;
		}

	public FnConvtr(
		Func<TIn, object?, TRet> FnConv
		,Func<TRet, object?, TIn>? FnBack = null
	){
		this.FnConv = FnConv;
		this.FnBack = FnBack;
	}
	
	public FnConvtr(
		Func<TIn, TRet> FnConv
		,Func<TRet, TIn>? FnBack = null
	){
		this.FnConv = (a,b)=>FnConv(a);
		if(FnBack is not null){
			this.FnBack = (a,b)=>FnBack(a);
		}
	}

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if(FnConv == null){
			return AvaloniaProperty.UnsetValue;
		}
		//直強轉 勿用is匹配、緣null is xxx旹恆返false、縱xxx可潙null
		try{
			return FnConv.Invoke((TIn)value!, parameter);
		}
		catch (System.Exception e){
			OnErr?.Invoke(e);
		}
		return AvaloniaProperty.UnsetValue;

	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if(FnBack == null){
			return AvaloniaProperty.UnsetValue;
		}
		try{
			return FnBack.Invoke((TRet)value!, parameter);
		}
		catch (System.Exception e){
			OnErr?.Invoke(e);
		}
		return AvaloniaProperty.UnsetValue;

	}
}





// ///

// /// <typeparam name="TIn">對應ViewModel</typeparam>
// /// <typeparam name="TRet">對應View</typeparam>
// public partial class SimpleFnConvtr<TIn, TRet>
// 	:IValConvtrWithErr
// {
// 	public Func<TIn, TRet>? FnConv{get;set;}
// 	public Func<TRet, TIn>? FnBack{get;set;}
// 	public Func<Exception, obj?>? OnErr{get;set;} = e => { Console.Error.WriteLine(e); return null; };
// 		public SimpleFnConvtr(Func<TIn, TRet> FnConv){
// 			this.FnConv = FnConv;
// 		}

// 	public SimpleFnConvtr(
// 		Func<TIn, TRet> FnConv
// 		,Func<TRet, TIn>? FnBack = null
// 	){
// 		this.FnConv = FnConv;
// 		this.FnBack = FnBack;
// 	}

// 	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
// 		if(FnConv == null){
// 			return AvaloniaProperty.UnsetValue;
// 		}
// 		//直強轉 勿用is匹配、緣null is xxx旹恆返false、縱xxx可潙null
// 		try{
// 			return FnConv.Invoke((TIn)value!);
// 		}catch(Exception e){
// 			OnErr?.Invoke(e);
// 			return AvaloniaProperty.UnsetValue;
// 		}

// 	}

// 	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
// 		if(FnBack == null){
// 			return AvaloniaProperty.UnsetValue;
// 		}
// 		try{
// 			return FnBack.Invoke((TRet)value!);
// 		}
// 		catch (System.Exception e){
// 			OnErr?.Invoke(e);
// 		}
// 		return AvaloniaProperty.UnsetValue;
// 	}
// }
