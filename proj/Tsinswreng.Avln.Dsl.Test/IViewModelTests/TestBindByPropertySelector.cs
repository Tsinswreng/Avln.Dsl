using Avalonia.Controls;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.IViewModelTests;

public partial class TestIViewModel{
	/// 測試 Bind(Expression<Func<TCtrl, object?>>...) 重載，順便覆蓋 bool? 屬性的 Prop 解析。
	public void RegisterBindByPropertySelector(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestIViewModel),
			[typeof(global::Tsinswreng.Avln.Dsl.ExtnIViewModel)],
			[nameof(global::Tsinswreng.Avln.Dsl.ExtnIViewModel.Bind)],
			"IViewModel_BindSelector"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Bind_PropertySelectorOverload_BindsCurrentValue", async(o)=>{
			var vm = new SampleVm{Name = "selector-bind"};
			var tb = new TextBlock();
			var binding = vm.Bind(
				tb,
				x=>x.Text,
				x=>x.Name,
				Source: vm
			);
			T(binding is not null);
			T(tb.Text == "selector-bind");
			return null;
		});

		R("Bind_PropertySelectorOverload_HandlesNullableBoolProperty", async(o)=>{
			var vm = new SampleVm{IsEnabledFlag = true};
			var cb = new CheckBox();
			var binding = vm.Bind(
				cb,
				x=>x.IsChecked,
				x=>x.IsEnabledFlag,
				Source: vm
			);
			T(binding is not null);
			T(cb.IsChecked == true);
			return null;
		});
	}
}
