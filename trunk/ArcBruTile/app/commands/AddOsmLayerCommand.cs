﻿using System.Runtime.InteropServices;
using BrutileArcGIS.Properties;
using ESRI.ArcGIS.ADF.BaseClasses;
using BrutileArcGIS.lib;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace BrutileArcGIS.commands
{
    [ProgId("AddOsmLayerCommand1")]
    public sealed class AddOsmLayerCommand : BaseCommand
    {
        private IApplication _application;

        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            _application = hook as IApplication;

            if (hook is IMxApplication)
                m_enabled = true;
            else
                m_enabled = false;
        }

        public AddOsmLayerCommand()
        {
            m_category = "BruTile";
            m_caption = "&Mapnik";
            m_message = "AddOsmMapnik map";
            m_toolTip = m_caption;
            m_name = "AddOsmMapnikLayerCommand";
            m_bitmap = Resources.download;

        }

        public override void OnClick()
        {
            const string url = "https://bertt.github.io/wmts/capabilities/osm.xml";
            // var wmtsLayer = WmtsHelper.GetWmtsLayerArcGIS(url, "OpenStreetMap Mapnik", "osm-mapnik");
            var wmtsLayer = WmtsHelper.GetWmtsLayer(_application, "png", url, "OpenStreetMap Mapnik", "osm-mapnik");
            var mxdoc = (IMxDocument)_application.Document;
            var map = mxdoc.FocusMap;
            ((IMapLayers)map).InsertLayer(wmtsLayer, true, 0);
        }
    }
}
