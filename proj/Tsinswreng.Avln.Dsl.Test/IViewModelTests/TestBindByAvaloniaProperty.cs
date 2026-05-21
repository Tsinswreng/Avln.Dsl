using Avalonia.Controls;
using Avalonia.Data;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.IViewModelTests;

public partial class TestIViewModel{
	/// 測試 Bind(AvaloniaProperty, ...) 重載。
	public void RegisterBindByAvaloniaProperty(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestIViewModel),
			[typeof(global::Tsinswreng.Avln.Dsl.ExtnIViewModel)],
			[nameof(global::Tsinswreng.Avln.Dsl.ExtnIViewModel.Bind)],
			"IViewModel_BindProp"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Bind_AvaloniaPropertyOverload_BindsCurrentValue", async(o)=>{
			var vm = new SampleVm{Name = "vm-bind"};
			var tb = new TextBlock();
			var binding = vm.Bind(
				tb,
				TextBlock.TextProperty,
				x=>x.Name,
				Mode: BindingMode.OneWay,
				Source: vm
			);
			T(binding is not null);
			T(tb.Text == "vm-bind");
			return null;
		});
	}
}
