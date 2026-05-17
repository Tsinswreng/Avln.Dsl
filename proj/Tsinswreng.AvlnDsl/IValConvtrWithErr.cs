using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
namespace Tsinswreng.AvlnDsl;
public interface IValConvtrWithErr:IValueConverter{
	public Func<Exception, obj?>? OnErr{get;set;}
}
