using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Styling;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.StyleBuilderTests;

public partial class TestStyleBuilder{
	/// Verifies StyleBuilder.Set with a binding.
	public void RegisterSetBinding(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestStyleBuilder),
			[typeof(global::Tsinswreng.Avln.Dsl.StyleBuilder<TextBlock>)],
			[nameof(global::Tsinswreng.Avln.Dsl.StyleBuilder<TextBlock>.Set)],
			"StyleBuilder_SetBinding"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("SetBinding_AppendsBindingSetter", async(o)=>{
			var binding = new Binding("Text");
			var builder = global::Tsinswreng.Avln.Dsl.Sty
				.Is<TextBlock>()
				.Set(x=>x.Text, binding);
			var style = builder.Build();
			T(style.Setters.Count == 1);
			var setter = (Setter)style.Setters[0];
			T(ReferenceEquals(setter.Property, TextBlock.TextProperty));
			T(ReferenceEquals(setter.Value, binding));
			return null;
		});
	}
}
