﻿using System.Runtime.InteropServices;
using BrutileArcGIS.lib;
using BrutileArcGIS.Lib;
using BrutileArcGIS.Properties;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;

namespace BrutileArcGIS.commands
{
    [ProgId("AddNokiaTerrainLayerCommand")]
    public class AddNokiaTerrainLayerCommand : BaseCommand
    {
        private IApplication _application;

        public AddNokiaTerrainLayerCommand()
        {
            m_category = "BruTile";
            m_caption = "&Terrain";
            m_message = "Add Nokia Here Terrain";
            m_toolTip = m_caption;
            m_name = "AddNokiaTerrainLayerCommand";
            m_bitmap = Resources.download;
        }

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

        public override void OnClick()
        {
            const string url = "https://bertt.github.io/wmts/capabilities/nokia.xml";
            var wmtsLayer = WmtsHelper.GetWmtsLayer(_application, "png", url, "Nokia terrain", "nokia-terrain");
            var mxdoc = (IMxDocument)_application.Document;
            var map = mxdoc.FocusMap;
            ((IMapLayers)map).InsertLayer(wmtsLayer, true, 0);

        }
    }
}
