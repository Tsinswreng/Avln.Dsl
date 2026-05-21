using Avalonia.Controls;
using Avalonia.Data;
using Tsinswreng.Avln.Dsl.Test.Support;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試 CBind、控制擴展屬性 Init/Bind，以及 Grid.SetRowDefs/SetColDefs。
	public void RegisterBindingAndGrid(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[
				nameof(global::Tsinswreng.Avln.Dsl.Extn.CBind),
				nameof(RegisterBindingAndGrid)
			],
			"Extn_BindingGrid"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("CBind_BindsCurrentValue", async(o)=>{
			var vm = new SampleVm{Name = "omega"};
			var tb = new TextBlock();
			var binding = global::Tsinswreng.Avln.Dsl.Extn.CBind<SampleVm>(
				tb,
				TextBlock.TextProperty,
				x=>x.Name,
				Source: vm
			);
			T(binding is not null);
			T(tb.Text == "omega");
			return null;
		});

		R("ControlInit_AssignsProperties", async(o)=>{
			var btn = new Button();
			btn.Init = x=>{
				x.Content = "init";
				x.Width = 88;
			};
			T(btn.Content is string s && s == "init");
			T(btn.Width == 88);
			return null;
		});

		R("ControlBind_PropertySetterBindsValue", async(o)=>{
			var vm = new SampleVm{Name = "binder"};
			var tb = new TextBlock();
			tb.Bind = (
				TextBlock.TextProperty,
				global::Tsinswreng.Avln.Dsl.CBE.Mk<SampleVm>(x=>x.Name, Source: vm)
			);
			T(tb.Text == "binder");
			return null;
		});

		R("GridSetRowDefs_AssignsDefinitions", async(o)=>{
			var grid = new Grid();
			var returned = grid.SetRowDefs([
				new RowDefinition(1, GridUnitType.Auto),
				new RowDefinition(2, GridUnitType.Star)
			]);
			T(ReferenceEquals(returned, grid));
			T(grid.RowDefinitions.Count == 2);
			return null;
		});

		R("GridSetColDefs_AssignsDefinitions", async(o)=>{
			var grid = new Grid();
			var returned = grid.SetColDefs([
				new ColumnDefinition(1, GridUnitType.Pixel),
				new ColumnDefinition(3, GridUnitType.Star)
			]);
			T(ReferenceEquals(returned, grid));
			T(grid.ColumnDefinitions.Count == 2);
			return null;
		});
	}
}
