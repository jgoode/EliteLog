//using EDDiscovery.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteModels {
    public class SystemPosition
    {
        #region Properties
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public int Nr { get; set; }
        public int BodyNr { get; set; }
        public SystemClass CurSystem { get; set; }
        public SystemClass PrevSystem { get; set; }
        public string strDistance { get; set; }
        #endregion

        #region Static Methods
        static public SystemPosition Parse(DateTime lasttime, string line) {
            SystemPosition sp = new SystemPosition();

            try {
                if (line.Length < 15)
                    return null;

                if (line.StartsWith("<data>"))
                    return null;

                string str = line.Substring(1, 2);

                int hour = int.Parse(str);
                int min = int.Parse(line.Substring(4, 2));
                int sec = int.Parse(line.Substring(7, 2));

                sp.Time = new DateTime(lasttime.Year, lasttime.Month, lasttime.Day, hour, min, sec);

                if (sp.Time.Subtract(lasttime).TotalHours < -12)
                    sp.Time = sp.Time.AddDays(1);

                str = line.Substring(18, line.IndexOf(" Body:") - 19);

                sp.Nr = int.Parse(str.Substring(0, str.IndexOf("(")));
                sp.Name = str.Substring(str.IndexOf("(") + 1);
                return sp;
            } catch {
                return null;
            }
        } 
        #endregion
    }
}
