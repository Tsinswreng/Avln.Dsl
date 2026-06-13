using System.Collections;
using Avalonia.Controls;
using Avalonia.Styling;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試四種 A 擴展，保證返回原集合並完成加入與初始化。
	public void RegisterCollectionAdds(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[nameof(global::Tsinswreng.Avln.Dsl.Extn.A)],
			nameof(global::Tsinswreng.Avln.Dsl.Extn.A)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("NonGenericList_AddsChildAndRunsInit", async(o)=>{
			IList list = new ArrayList();
			var child = new Button();
			var inited = false;
			var returned = global::Tsinswreng.Avln.Dsl.Extn.A(list, child, x=>{
				inited = true;
				x.Content = "btn";
			});
			T(ReferenceEquals(returned, list));
			T(list.Count == 1);
			T(ReferenceEquals(list[0], child));
			T(inited);
			return null;
		});

		R("StyleCollection_AddsStyle", async(o)=>{
			ICollection<IStyle> styles = new Styles();
			var style = new Style(x=>x.Is<Button>());
			var returned = global::Tsinswreng.Avln.Dsl.Extn.A(styles, style);
			T(ReferenceEquals(returned, styles));
			T(styles.Count == 1);
			return null;
		});

		R("ControlCollection_AddsControl", async(o)=>{
			ICollection<Control> controls = new List<Control>();
			var tb = new TextBlock();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.A(controls, tb);
			T(ReferenceEquals(returned, controls));
			T(controls.Count == 1);
			return null;
		});

		R("Controls_AddsControl", async(o)=>{
			var controls = new Controls();
			var tb = new TextBlock();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.A(controls, tb);
			T(ReferenceEquals(returned, controls));
			T(controls.Count == 1);
			return null;
		});

		R("Panel_AddsChildIntoChildren", async(o)=>{
			var panel = new StackPanel();
			var tb = new TextBlock();
			var returned = global::Tsinswreng.Avln.Dsl.Extn.A(panel, tb);
			T(ReferenceEquals(returned, panel));
			T(panel.Children.Count == 1);
			T(ReferenceEquals(panel.Children[0], tb));
			return null;
		});
	}
}
