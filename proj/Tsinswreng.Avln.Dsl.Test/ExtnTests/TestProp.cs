using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試 Prop 對普通屬性、值類型裝箱屬性與異常分支的解析。
	public void RegisterProp(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[nameof(global::Tsinswreng.Avln.Dsl.Extn.Prop)],
			nameof(global::Tsinswreng.Avln.Dsl.Extn.Prop)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Prop_ResolvesTextProperty", async(o)=>{
			var tb = new TextBlock();
			var prop = global::Tsinswreng.Avln.Dsl.Extn.Prop(tb, x=>x.Text);
			T(ReferenceEquals(prop, TextBlock.TextProperty));
			return null;
		});

		R("Prop_UnwrapsNullableValueTypeProperty", async(o)=>{
			var cb = new CheckBox();
			var prop = global::Tsinswreng.Avln.Dsl.Extn.Prop(cb, x=>x.IsChecked);
			T(ReferenceEquals(prop, ToggleButton.IsCheckedProperty));
			return null;
		});

		R("Prop_InvalidSelector_Throws", async(o)=>{
			var tb = new TextBlock();
			var thrown = false;
			try{
				_ = global::Tsinswreng.Avln.Dsl.Extn.Prop(tb, x=>x.Text+"!");
			}
			catch(ArgumentException){
				thrown = true;
			}
			T(thrown);
			return null;
		});

		R("Prop_MissingAvaloniaProperty_Throws", async(o)=>{
			var obj = new PlainAvaloniaObject();
			var thrown = false;
			try{
				_ = global::Tsinswreng.Avln.Dsl.Extn.Prop(obj, x=>x.PlainClrOnly);
			}
			catch(InvalidOperationException){
				thrown = true;
			}
			T(thrown);
			return null;
		});
	}
}
