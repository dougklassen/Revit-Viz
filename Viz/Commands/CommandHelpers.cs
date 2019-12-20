using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DougKlassen.Revit.Viz.Commands
{
    public static class CommandHelpers
    {
        public static ICollection<Element> GetAllModelElements(this Document dbDoc)
        {
            ICollection<Element> elements = new List<Element>();

            return elements;
        }
    }
}
