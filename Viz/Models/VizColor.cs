﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DougKlassen.Revit.Viz
{
    public class VizColor
    {
        public Byte? Red { get; set; }
        public Byte? Green { get; set; }
        public Byte? Blue { get; set; }

        public VizColor()
        {
            Red = null;
            Green = null;
            Blue = null;
        }

        public VizColor(Color color)
        {
            if (color.IsValid)
            {
                Red = color.Red;
                Green = color.Green;
                Blue = color.Blue;
            }
            else
            {
                Red = null;
                Green = null;
                Blue = null;
            }
        }

        public Color GetColor()
        {
            if(Red.HasValue && Green.HasValue && Blue.HasValue)
            {
                return new Color(Red.Value, Green.Value, Blue.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
