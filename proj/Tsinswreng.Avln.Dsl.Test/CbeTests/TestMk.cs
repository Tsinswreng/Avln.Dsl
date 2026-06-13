using Avalonia.Controls;
using Avalonia.Data;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.CbeTests;

public partial class TestCBE{
	/// 測試 Mk 返回的 CompiledBindingExtension 是否能帶上配置並實際生效。
	public void RegisterMk(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestCBE),
			[typeof(global::Tsinswreng.Avln.Dsl.CBE)],
			[nameof(global::Tsinswreng.Avln.Dsl.CBE.Mk)],
			nameof(global::Tsinswreng.Avln.Dsl.CBE.Mk)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Ctor_UsesProvidedPathToBind", async(o)=>{
			var vm = new SampleVm{Name = "ctor"};
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm>(x=>x.Name);
			var binding = new global::Tsinswreng.Avln.Dsl.CBE(path){
				Source = vm
			};
			var tb = new TextBlock();
			tb.Bind(TextBlock.TextProperty, binding);
			T(tb.Text == "ctor");
			return null;
		});

		R("Mk_AssignsBindingProperties", async(o)=>{
			var vm = new SampleVm{Name = "gamma"};
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm>(x=>x.Name);
			var binding = global::Tsinswreng.Avln.Dsl.CBE.Mk<SampleVm>(
				x=>x.Name,
				Mode: BindingMode.TwoWay,
				ConverterParameter: "p",
				Path: path,
				Source: vm,
				DataType: typeof(SampleVm)
			);
			T(binding.Mode == BindingMode.TwoWay);
			T(ReferenceEquals(binding.Source, vm));
			T(binding.ConverterParameter is string s && s == "p");
			T(binding.DataType == typeof(SampleVm));
			T(binding.Path is not null);
			return null;
		});

		R("Mk_BindsPropertyValue", async(o)=>{
			var vm = new SampleVm{Name = "delta"};
			var tb = new TextBlock();
			tb.Bind(
				TextBlock.TextProperty,
				global::Tsinswreng.Avln.Dsl.CBE.Mk<SampleVm>(x=>x.Name, Source: vm)
			);
			T(tb.Text == "delta");
			return null;
		});
	}
}
