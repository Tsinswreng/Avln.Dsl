using Avalonia.Controls;
using Avalonia.Layout;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試 HAlign / HCAlign / VAlign / VCAlign 的值重載與函數重載。
	public void RegisterAlign(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[
				nameof(global::Tsinswreng.Avln.Dsl.Extn.HAlign),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.HCAlign),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.VAlign),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.VCAlign)
			],
			"Extn_Align"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("HAlign_SetsHorizontalAlignment", async(o)=>{
			var tb = new TextBlock();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.HAlign(tb, HorizontalAlignment.Right);
			T(ReferenceEquals(returned, tb));
			T(tb.HorizontalAlignment == HorizontalAlignment.Right);
			return null;
		});

		R("HAlign_FnOverload_SetsHorizontalAlignment", async(o)=>{
			var tb = new TextBlock();
			global::Tsinswreng.Avln.Dsl.Extn.HAlign(tb, x=>x.Center);
			T(tb.HorizontalAlignment == HorizontalAlignment.Center);
			return null;
		});

		R("HCAlign_SetsHorizontalContentAlignment", async(o)=>{
			var btn = new Button();
			global::Tsinswreng.Avln.Dsl.Extn.HCAlign(btn, HorizontalAlignment.Left);
			T(btn.HorizontalContentAlignment == HorizontalAlignment.Left);
			return null;
		});

		R("HCAlign_FnOverload_SetsHorizontalContentAlignment", async(o)=>{
			var btn = new Button();
			global::Tsinswreng.Avln.Dsl.Extn.HCAlign(btn, x=>x.Stretch);
			T(btn.HorizontalContentAlignment == HorizontalAlignment.Stretch);
			return null;
		});

		R("VAlign_SetsVerticalAlignment", async(o)=>{
			var tb = new TextBlock();
			global::Tsinswreng.Avln.Dsl.Extn.VAlign(tb, VerticalAlignment.Bottom);
			T(tb.VerticalAlignment == VerticalAlignment.Bottom);
			return null;
		});

		R("VAlign_FnOverload_SetsVerticalAlignment", async(o)=>{
			var tb = new TextBlock();
			global::Tsinswreng.Avln.Dsl.Extn.VAlign(tb, x=>x.Top);
			T(tb.VerticalAlignment == VerticalAlignment.Top);
			return null;
		});

		R("VCAlign_SetsVerticalContentAlignment", async(o)=>{
			var btn = new Button();
			global::Tsinswreng.Avln.Dsl.Extn.VCAlign(btn, VerticalAlignment.Center);
			T(btn.VerticalContentAlignment == VerticalAlignment.Center);
			return null;
		});

		R("VCAlign_FnOverload_SetsVerticalContentAlignment", async(o)=>{
			var btn = new Button();
			global::Tsinswreng.Avln.Dsl.Extn.VCAlign(btn, x=>x.Stretch);
			T(btn.VerticalContentAlignment == VerticalAlignment.Stretch);
			return null;
		});
	}
}
