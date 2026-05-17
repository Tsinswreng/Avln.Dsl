#import "@preview/tsinswreng-auto-heading:0.1.0": auto-heading
#let H = auto-heading;

#H[Tsinswreng.Avln.Dsl][
	Tsinswreng.Avln.Dsl 提供一組偏 fluent / DSL 風格的 Avalonia UI 拼裝輔助。

	它的目標不是替代 XAML，而是讓你在純 C\# 中更順手地：

	- 添加子控件
	- 設置樣式與對齊
	- 建立 compiled binding
	- 在代碼中組裝視圖樹

	#H[安裝][
		```bash
		dotnet add package Tsinswreng.Avln.Dsl --version 0.0.1-alpha
		```
	]

	#H[主要內容][
		- `Extn`
		- `IView<TCtx>`
		- `CBind(...)`
		- `A(...)`
		- 對齊與樣式輔助方法
	]

	#H[示例][
		```csharp
		var panel = new StackPanel()
			.A(new TextBlock(), o => {
				o.Text = "Hello";
			})
			.A(new Button(), o => {
				o.Content = "Click";
			});
		```
	]
]
