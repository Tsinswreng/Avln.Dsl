using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.PsdClsTests;

/// 測試僞類常量值是否符合預期。
public class TestPsdCls: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		var register = Node.MkTestFnRegister(
			typeof(TestPsdCls),
			[typeof(global::Tsinswreng.Avln.Dsl.PsdCls)],
			[],
			nameof(TestPsdCls)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("PointerOver_HasColonPrefix", async(o)=>{
			T(global::Tsinswreng.Avln.Dsl.PsdCls.pointerover == ":pointerover");
			return null;
		});

		R("Focus_HasColonPrefix", async(o)=>{
			T(global::Tsinswreng.Avln.Dsl.PsdCls.focus == ":focus");
			return null;
		});

		R("FocusWithin_UsesAvaloniaPseudoClassName", async(o)=>{
			T(global::Tsinswreng.Avln.Dsl.PsdCls.focus_within == ":focus-within");
			return null;
		});

		R("Pressed_HasColonPrefix", async(o)=>{
			T(global::Tsinswreng.Avln.Dsl.PsdCls.pressed == ":pressed");
			return null;
		});

		return Node;
	}
}
