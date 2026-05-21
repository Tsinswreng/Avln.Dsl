using Avalonia.Controls;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnItemsControlTests;

/// 測試 ItemsControl 相關 DSL。
public class TestExtnItemsControl: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		var register = Node.MkTestFnRegister(
			typeof(TestExtnItemsControl),
			[typeof(global::Tsinswreng.Avln.Dsl.ExtnItemsControl)],
			[
				nameof(RegisterTestsInto),
				nameof(global::Tsinswreng.Avln.Dsl.ExtnItemsControl),
				nameof(global::Tsinswreng.Avln.Dsl.ExtnItemsControl)
			],
			nameof(TestExtnItemsControl)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("SetItemTemplate_AssignsTemplate", async(o)=>{
			var items = new ItemsControl();
			var returned = items.SetItemTemplate<string>((ele, ns)=>{
				return new TextBlock{
					Text = ele
				};
			});
			T(ReferenceEquals(returned, items));
			T(items.ItemTemplate is not null);
			return null;
		});

		R("SetItemsPanel_AssignsTemplate", async(o)=>{
			var items = new ItemsControl();
			var returned = items.SetItemsPanel(()=>{
				return new StackPanel();
			});
			T(ReferenceEquals(returned, items));
			T(items.ItemsPanel is not null);
			return null;
		});

		return Node;
	}
}
