using System.Drawing;

namespace BrutileArcGIS.Lib
{
    public class Transform
    {
        float _resolutionX;
        float _resolutionY;
        PointF _center;
        float _width;
        float _height;
        BruTile.Extent _extent;

        public Transform(PointF center, float resolutionX, float resolutionY, float width, float height)
        {
            _center = center;
            _resolutionX = resolutionX;
            _resolutionY = resolutionY;
            _width = width;
            _height = height;
            UpdateExtent();
        }

        public float Resolution
        {
            set
            {
                _resolutionX = value;
                UpdateExtent();
            }
            get
            {
                return _resolutionX;
            }
        }

        public PointF Center
        {
            set
            {
                _center = value;
                UpdateExtent();
            }
        }

        public float Width
        {
            set
            {
                _width = value;
                UpdateExtent();
            }
        }

        public float Height
        {
            set
            {
                _height = value;
                UpdateExtent();
            }
        }

        public BruTile.Extent Extent
        {
            get { return _extent; }
        }

        public PointF WorldToMap(double x, double y)
        {
            return new PointF((float)(x - _extent.MinX) / _resolutionX, (float)(_extent.MaxY - y) / _resolutionX);
        }

        public PointF MapToWorld(double x, double y)
        {
            return new PointF((float)(_extent.MinX + x) * _resolutionX, (float)(_extent.MaxY - y) * _resolutionX);
        }

        public RectangleF WorldToMap(double x1, double y1, double x2, double y2)
        {
            var point1 = WorldToMap(x1, y1);
            var point2 = WorldToMap(x2, y2);
            return new RectangleF(point1.X, point2.Y, point2.X - point1.X, point1.Y - point2.Y);
        }

       private void UpdateExtent()
        {
            var spanX = _width * _resolutionX;
            var spanY = _height * _resolutionX;
            _extent = new BruTile.Extent(_center.X - spanX * 0.5f, _center.Y - spanY * 0.5f,
              _center.X + spanX * 0.5f, _center.Y + spanY * 0.5f);
        }
    }
}
