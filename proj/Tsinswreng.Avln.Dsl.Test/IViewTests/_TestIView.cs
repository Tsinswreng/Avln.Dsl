using Avalonia.Controls;
using Avalonia.Data;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.IViewTests;

/// 測試 IView 擴展 Bind。
public class TestIView: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		var register = Node.MkTestFnRegister(
			typeof(TestIView),
			[typeof(global::Tsinswreng.Avln.Dsl.ExtnIView)],
			[nameof(global::Tsinswreng.Avln.Dsl.ExtnIView.Bind)],
			nameof(TestIView)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Ctx_CanRoundTripValue", async(o)=>{
			var view = new SampleView();
			var vm = new SampleVm{Name = "ctx"};
			view.Ctx = vm;
			T(ReferenceEquals(view.Ctx, vm));
			return null;
		});

		R("Bind_AvaloniaPropertyOverload_BindsCurrentValue", async(o)=>{
			var view = new SampleView();
			var vm = new SampleVm{Name = "view-bind"};
			var tb = new TextBlock();
			var binding = view.Bind(
				tb,
				TextBlock.TextProperty,
				x=>x.Name,
				Mode: BindingMode.OneWay,
				Source: vm
			);
			T(binding is not null);
			T(tb.Text == "view-bind");
			return null;
		});

		return Node;
	}
}
