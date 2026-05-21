using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.CbeTests;

public partial class TestCBE{
	/// 測試 CBE.Pth 的各種表達式路徑，包括:
	/// 1. 普通屬性路徑
	/// 2. 直接對象綁定
	/// 3. 非法表達式與類型不匹配
	/// 4. 運行時對象類型不匹配時不再拋異常
	public void RegisterPth(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestCBE),
			[typeof(global::Tsinswreng.Avln.Dsl.CBE)],
			[nameof(global::Tsinswreng.Avln.Dsl.CBE.Pth)],
			nameof(global::Tsinswreng.Avln.Dsl.CBE.Pth)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("PropertyPath_BindsCurrentValue", async(o)=>{
			var vm = new SampleVm{Name = "alpha"};
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm>(x=>x.Name);
			var binding = new global::Tsinswreng.Avln.Dsl.CBE(path){
				Source = vm
			};
			var tb = new TextBlock();
			tb.Bind(TextBlock.TextProperty, binding);
			T(tb.Text == "alpha");
			return null;
		});

		R("ObjectPath_BindsWholeObject", async(o)=>{
			var vm = new SampleVm{Name = "whole"};
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm, SampleVm>(x=>x);
			var binding = new global::Tsinswreng.Avln.Dsl.CBE(path){
				Source = vm
			};
			var tb = new TextBlock();
			tb.Bind(StyledElement.DataContextProperty, binding);
			T(ReferenceEquals(tb.DataContext, vm));
			return null;
		});

		R("ValueTypeProperty_UnwrapsConvertNode", async(o)=>{
			var vm = new SampleVm{IsEnabledFlag = true};
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm>(x=>x.IsEnabledFlag);
			var binding = new global::Tsinswreng.Avln.Dsl.CBE(path){
				Source = vm
			};
			var cb = new CheckBox();
			cb.Bind(ToggleButton.IsCheckedProperty, binding);
			T(cb.IsChecked == true);
			return null;
		});

		R("UnsupportedExpression_Throws", async(o)=>{
			var thrown = false;
			try{
				_ = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm, object?>(x=>x.Name+"!");
			}
			catch(ArgumentException){
				thrown = true;
			}
			T(thrown);
			return null;
		});

		R("ObjectBinding_TypeMismatch_Throws", async(o)=>{
			var thrown = false;
			try{
				_ = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm, OtherVm>(x=>x);
			}
			catch(InvalidOperationException){
				thrown = true;
			}
			T(thrown);
			return null;
		});

		R("RuntimeTypeMismatch_ReturnsUnsetInsteadOfThrowing", async(o)=>{
			var path = global::Tsinswreng.Avln.Dsl.CBE.Pth<SampleVm>(x=>x.Name);
			var binding = new global::Tsinswreng.Avln.Dsl.CBE(path){
				Source = new OtherVm{OtherName = "beta"}
			};
			var tb = new TextBlock{
				Text = "keep-default"
			};
			tb.Bind(TextBlock.TextProperty, binding);
			T(tb.Text == "keep-default");
			return null;
		});
	}
}
