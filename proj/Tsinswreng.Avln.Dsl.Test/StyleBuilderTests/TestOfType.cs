using Avalonia.Controls;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.StyleBuilderTests;

public partial class TestStyleBuilder{
	/// Verifies basic behavior of Sty.OfType.
	public void RegisterOfType(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestStyleBuilder),
			[typeof(global::Tsinswreng.Avln.Dsl.Sty)],
			[nameof(global::Tsinswreng.Avln.Dsl.Sty.OfType)],
			"StyleBuilder_OfType"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("OfType_BuildReturnsSameInstance", async(o)=>{
			var builder = global::Tsinswreng.Avln.Dsl.Sty.OfType<Button>();
			var style = builder.Build();
			T(ReferenceEquals(builder, style));
			return null;
		});

		R("OfType_CreatesSelector", async(o)=>{
			var builder = global::Tsinswreng.Avln.Dsl.Sty.OfType<Button>();
			var style = builder.Build();
			T(style.Selector is not null);
			return null;
		});
	}
}
