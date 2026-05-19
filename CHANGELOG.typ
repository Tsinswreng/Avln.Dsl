#import "@preview/tsinswreng-auto-heading:0.1.0": auto-heading
#let H = auto-heading;

#H[v1.0.1-alpha][
	#H[➕Grid增`SetRowDefs`等寫法][
		[2026-05-19T22:00:23.645+08:00]
	]
	
	#H[CBE反射改用 PropertyInfo][
		[2026-05-19T21:56:51.007+08:00_W21-2]
	]
	
	#H[❌`Extn`類中刪除以下API][
		2026-05-19T11:59:55.689+08:00_W21-2
		```cs
			public static RowDefinition RowDef(double value, GridUnitType type){
				return new RowDefinition(value, type);
			}
			public static ColumnDefinition ColDef(double value, GridUnitType type){
				return new ColumnDefinition(value, type);
			}

		```
	]


]

#H[v0.0.1-alpha][
	- 2026-05-17T21:16:39.198+08:00_W20-7

]
