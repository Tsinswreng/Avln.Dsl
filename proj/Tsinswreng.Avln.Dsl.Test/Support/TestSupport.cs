using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Tsinswreng.Avln.Dsl.Test.Support;

/// 測試用 ViewModel: 覆蓋字串、可空布爾與直接對象綁定場景。
public partial class SampleVm
	:ObservableObject
	,global::Tsinswreng.Avln.Dsl.IViewModel
{
	public string Name{
		get;
		set{SetProperty(ref field, value);}
	} = "";

	public bool? IsEnabledFlag{
		get;
		set{SetProperty(ref field, value);}
	} = false;
}

/// 測試用異類型 DataContext，專門覆蓋 CBE 的運行時類型不匹配分支。
public partial class OtherVm: ObservableObject{
	public string OtherName{
		get;
		set{SetProperty(ref field, value);}
	} = "";
}

/// 測試 IView 擴展方法使用的簡單實現。
public class SampleView: global::Tsinswreng.Avln.Dsl.IView<SampleVm>{
	public SampleVm? Ctx{get;set;}
}

/// 僅含 CLR 屬性、不含對應 AvaloniaProperty，用於測試 Prop 的異常分支。
public class PlainAvaloniaObject: AvaloniaObject{
	public string PlainClrOnly{get;set;} = "";
}

/// 測試 SetChild 用的最小 Decorator 子類。
public class SampleDecorator: Decorator{

}
