using System;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;

public class MainMapViewModel : BindableBase : INotifyPropertyChanged
{
	public MainMapViewModel() 
	{
		SetupMap();
	}
}
