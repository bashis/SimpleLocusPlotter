using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using info.lundin.math;
using System.Collections;

namespace LocusPlotter
{
    class Locus
    {
        public Locus()
        {

        }

        public Locus(string u, string v)
        {
            U = u;
            V = v;
        }
        public string U { get; set; }
        public string V { get; set; }

        public PointF? GetPoint(double w)
        {
            ExpressionParser parser = new ExpressionParser();

            Hashtable table = new Hashtable();
            table.Add("w", w.ToString());

            try
            {
                float u = Convert.ToSingle(parser.Parse(U, table));
                float v = Convert.ToSingle(parser.Parse(V, table));
                return new PointF(u, v);
            }
            catch (ParserException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
