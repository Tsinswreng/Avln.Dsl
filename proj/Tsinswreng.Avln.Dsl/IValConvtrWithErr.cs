using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
namespace Tsinswreng.Avln.Dsl;
public interface IValConvtrWithErr:IValueConverter{
	public Func<Exception, obj?>? OnErr{get;set;}
}
