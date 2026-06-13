using Avalonia.Controls;
using Avalonia.Styling;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.StyleBuilderTests;

public partial class TestStyleBuilder{
	/// Verifies StyleBuilder.Set with a direct value.
	public void RegisterSetValue(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestStyleBuilder),
			[typeof(global::Tsinswreng.Avln.Dsl.StyleBuilder<TextBlock>)],
			[nameof(global::Tsinswreng.Avln.Dsl.StyleBuilder<TextBlock>.Set)],
			"StyleBuilder_SetValue"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("SetValue_AppendsSetter", async(o)=>{
			var builder = global::Tsinswreng.Avln.Dsl.Sty
				.Is<TextBlock>()
				.Set(x=>x.Text, "alpha");
			var style = builder.Build();
			T(style.Setters.Count == 1);
			var setter = (Setter)style.Setters[0];
			T(ReferenceEquals(setter.Property, TextBlock.TextProperty));
			T(setter.Value is string s && s == "alpha");
			return null;
		});
	}
}
