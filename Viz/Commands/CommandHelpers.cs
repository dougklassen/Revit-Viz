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
        public static IEnumerable<Element> GetAllElements(this Document dbDoc)
        {
            IEnumerable<Element> allElements = new List<Element>();

            ElementFilter allElementsFilter = new LogicalOrFilter(new ElementIsElementTypeFilter(false), new ElementIsElementTypeFilter(true));
            allElements = new FilteredElementCollector(dbDoc).WherePasses(allElementsFilter);

            return allElements;
        }
    }
}
