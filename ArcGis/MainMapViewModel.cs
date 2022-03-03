using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace ArcGis
{
    public class MainMapViewModel : BindableBase
    {
        private Map _map;
        private GraphicsOverlayCollection _graphicsOverlays;

        public Map Map
        {
            get { return _map; }
            set
            {
                SetProperty(ref _map, value);
            }
        }
        public GraphicsOverlayCollection GraphicsOverlays
        {
            get { return _graphicsOverlays; }
            set
            {
                SetProperty(ref _graphicsOverlays, value);
            }
        }
        public MainMapViewModel()
        {
            SetupMap();
            CreateGraphics();
        }

        private void SetupMap()
        {
            // Create a new map with a 'topographic vector' basemap.
            this.Map = new Map(Basemap.CreateDarkGrayCanvasVector());
        }

        private void CreateGraphics()
        {
            // Create a new graphics overlay to contain a variety of graphics.
            var malibuGraphicsOverlay = new GraphicsOverlay();

            // Add the overlay to a graphics overlay collection.
            GraphicsOverlayCollection overlays = new GraphicsOverlayCollection
            {
                malibuGraphicsOverlay
            };

            // Set the view model's "GraphicsOverlays" property (will be consumed by the map view).
            this.GraphicsOverlays = overlays;

            List<MapPoint> polygonPoints = new List<MapPoint>();

            int radius = 5;
            int initialX = 0;
            int initialY = 0;

            for (int i = 0; i < 60; i++)
            {
                var x = initialX + (radius * Math.Cos((2 * Math.PI / 60) * i));
                var y = initialY + (radius * Math.Sin((2 * Math.PI / 60) * i));
                polygonPoints.Add(new MapPoint(x, y, SpatialReferences.Wgs84));
            }

            // Create polygon geometry.
            var mahouRivieraPolygon = new Polygon(polygonPoints);

            Envelope extentEnvelope = mahouRivieraPolygon.Extent;
            MapPoint centerPoint = extentEnvelope.GetCenter();

            Graphic textGraphic = new Graphic();
            // Create two text symbols
            TextSymbol textSymbol = new TextSymbol("String Aleatória", Color.Red, 22,
                HorizontalAlignment.Center, VerticalAlignment.Middle);
            textGraphic.Symbol = textSymbol;
            textGraphic.Geometry = centerPoint;
            //Add the two graphics to the map.

            // Create a fill symbol to display the polygon.
            var polygonSymbolOutline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.Blue, 2.0);
            var polygonFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.Transparent, polygonSymbolOutline);

            // Create a polygon graphic with the geometry and fill symbol.
            var polygonGraphic = new Graphic(mahouRivieraPolygon, polygonFillSymbol);

            // Add the polygon graphic to the graphics overlay.
            malibuGraphicsOverlay.Graphics.Add(polygonGraphic);
            malibuGraphicsOverlay.Graphics.Add(textGraphic);




        }
    }
}
