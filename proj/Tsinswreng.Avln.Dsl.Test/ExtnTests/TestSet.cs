using Avalonia;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

public partial class TestExtn{
	/// 測試 ref struct 擴展 Set。
	public void RegisterStructSet(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestExtn),
			[typeof(global::Tsinswreng.Avln.Dsl.Extn)],
			[nameof(RegisterStructSet)],
			"Extn_StructSet"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("StructSet_ReplacesValue", async(o)=>{
			Thickness value = default;
			value.Set(new Thickness(1, 2, 3, 4));
			T(value.Left == 1);
			T(value.Top == 2);
			T(value.Right == 3);
			T(value.Bottom == 4);
			return null;
		});
	}
}
