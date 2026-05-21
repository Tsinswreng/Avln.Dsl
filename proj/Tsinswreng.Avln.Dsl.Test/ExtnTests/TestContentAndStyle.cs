using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試 SetContent / SetChild / AddTo / Style.Set / 對齊助手類常量。
	public void RegisterContentAndStyle(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[
				nameof(global::Tsinswreng.Avln.Dsl.Extn.SetContent),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.SetChild),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.AddTo),
				nameof(global::Tsinswreng.Avln.Dsl.Extn.Set)
			],
			"Extn_ContentStyle"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("SetContent_AssignsContentAndRunsInit", async(o)=>{
			var host = new ContentControl();
			var content = new TextBlock();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.SetContent(host, content, x=>{
				x.Text = "content";
			});
			T(ReferenceEquals(returned, content));
			T(ReferenceEquals(host.Content, content));
			T(content.Text == "content");
			return null;
		});

		R("SetChild_AssignsDecoratorChild", async(o)=>{
			var host = new SampleDecorator();
			var child = new Border();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.SetChild(host, child, x=>{
				x.Width = 33;
			});
			T(ReferenceEquals(returned, child));
			T(ReferenceEquals(host.Child, child));
			T(child.Width == 33);
			return null;
		});

		R("AddTo_AddsStyleIntoStylesCollection", async(o)=>{
			var styles = new Styles();
			var style = new Style(x=>x.Is<Button>());
			var returned = global::Tsinswreng.Avln.Dsl.Extn.AddTo(style, styles, x=>{
				x.Setters.Add(new Setter(Control.TagProperty, "tag"));
			});
			T(ReferenceEquals(returned, style));
			T(styles.Count == 1);
			return null;
		});

		R("StyleSet_AppendsSetter", async(o)=>{
			var style = new Style(x=>x.Is<Button>());
			var returned = global::Tsinswreng.Avln.Dsl.Extn.Set(style, Control.TagProperty, "marker");
			T(ReferenceEquals(returned, style));
			T(style.Setters.Count == 1);
			return null;
		});

		R("AlignmentHelperClasses_ExposeExpectedValues", async(o)=>{
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsHorizontalAlignment.Inst.Left == HorizontalAlignment.Left);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsHorizontalAlignment.Inst.Center == HorizontalAlignment.Center);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsHorizontalAlignment.Inst.Right == HorizontalAlignment.Right);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsHorizontalAlignment.Inst.Stretch == HorizontalAlignment.Stretch);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsVerticalAlignment.Inst.Top == VerticalAlignment.Top);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsVerticalAlignment.Inst.Center == VerticalAlignment.Center);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsVerticalAlignment.Inst.Bottom == VerticalAlignment.Bottom);
			T(global::Tsinswreng.Avln.Dsl.Extn.ClsVerticalAlignment.Inst.Stretch == VerticalAlignment.Stretch);
			return null;
		});
	}
}
