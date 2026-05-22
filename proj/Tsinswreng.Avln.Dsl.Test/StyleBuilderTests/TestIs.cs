using Avalonia.Controls;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.StyleBuilderTests;

public partial class TestStyleBuilder{
	/// Verifies basic behavior of Sty.Is.
	public void RegisterIs(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestStyleBuilder),
			[typeof(global::Tsinswreng.Avln.Dsl.Sty)],
			[nameof(global::Tsinswreng.Avln.Dsl.Sty.Is)],
			"StyleBuilder_Is"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Is_BuildReturnsSameInstance", async(o)=>{
			var builder = global::Tsinswreng.Avln.Dsl.Sty.Is<Button>();
			var style = builder.Build();
			T(ReferenceEquals(builder, style));
			return null;
		});

		R("Is_CreatesSelector", async(o)=>{
			var builder = global::Tsinswreng.Avln.Dsl.Sty.Is<Button>();
			var style = builder.Build();
			T(style.Selector is not null);
			return null;
		});
	}
}
