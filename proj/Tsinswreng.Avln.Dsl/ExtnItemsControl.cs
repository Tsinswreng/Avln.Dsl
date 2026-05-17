using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Tsinswreng.Avln.Dsl;
using TSelf = ItemsControl;
public static class ExtnItemsControl{
	extension(TSelf z)
		//若用泛型TSelf則擴展ʹ泛型方法ʸ需手動指定兩個泛型參數
		//where TSelf: ItemsControl
	{
		/// <typeparam name="TEle">須同於ItemsSourceʹ元素ʹ類型、否則內ʹ匿名函數ˋ不珩</typeparam>
		public TSelf SetItemTemplate<TEle>(
			Func<TEle, INameScope, Control?> build, bool supportsRecycling = false
		){
			z.ItemTemplate = new FuncDataTemplate<TEle>(build, supportsRecycling);
			return z;
		}
		public TSelf SetItemsPanel(Func<Panel?> Fn){
			z.ItemsPanel = new FuncTemplate<Panel?>(Fn);
			return z;
		}
	}
}
