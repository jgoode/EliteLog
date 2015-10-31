using System;
using System.Data;
using EliteModels.Enums;
using System.Diagnostics;

namespace EliteModels {


    [DebuggerDisplay("System {name} ({x,nq},{y,nq},{z,nq})")]
    public class SystemClass {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string SearchName { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Cr { get; set; }
        public string CommanderCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CommanderUpdate { get; set; }
        public DateTime UpdateDate { get; set; }
        public SystemStatusEnum Status { get; set; }
        public string Note { get; set; }
        public int IdEddb { get; set; }
        public string Faction { get; set; }
        public long Population { get; set; }
        public EDGovernment Government { get; set; }
        public EDAllegiance Allegiance { get; set; }
        public EDState State { get; set; }
        public EDSecurity Security { get; set; }
        public EDEconomy PrimaryEconomy { get; set; }
        public int NeedsPermit { get; set; }
        public int EddbUpdatedAt { get; set; }
        public bool HasCoordinate {
            get {
                return (!double.IsNaN(X));
            }
        }
        #endregion

        #region Ctor
        public SystemClass() {
        }

        public SystemClass(string name) {
            Name = name;
            SearchName = Name.ToLower();
            Status = SystemStatusEnum.Unknown;
            X = double.NaN;
            Y = double.NaN;
            Z = double.NaN;
        }

        public SystemClass(DataRow dr) {
            try {
                Object o;

                Id = (int)(long)dr["id"];
                Name = (string)dr["name"];
                SearchName = Name.ToLower();
                Cr = (int)(long)dr["cr"];
                if (DBNull.Value == dr["x"]) {
                    X = double.NaN;
                    Y = double.NaN;
                    Z = double.NaN;
                } else {
                    X = (double)dr["x"];
                    Y = (double)dr["y"];
                    Z = (double)dr["z"];
                    CommanderCreate = dr["commandercreate"].ToString();
                    if (CommanderCreate.Length > 0)
                        CreateDate = (DateTime)dr["createdate"];
                    else
                        CreateDate = new DateTime(1980, 1, 1);

                    CommanderUpdate = (string)dr["commanderupdate"].ToString();
                    if (CommanderUpdate.Length > 0)
                        UpdateDate = (DateTime)dr["updatedate"];
                    else
                        UpdateDate = new DateTime(1980, 1, 1);


                }

                Status = (SystemStatusEnum)((long)dr["status"]);
                Note = dr["Note"].ToString();

                o = dr["id_eddb"];
                IdEddb = o == DBNull.Value ? 0 : (int)((long)o);

                o = dr["faction"];
                Faction = o == DBNull.Value ? null : (string)o;

                o = dr["government_id"];
                Government = o == DBNull.Value ? EDGovernment.Unknown : (EDGovernment)((long)o);

                o = dr["allegiance_id"];
                Allegiance = o == DBNull.Value ? EDAllegiance.Unknown : (EDAllegiance)((long)o);

                o = dr["primary_economy_id"];
                PrimaryEconomy = o == DBNull.Value ? EDEconomy.Unknown : (EDEconomy)((long)o);

                o = dr["security"];
                Security = o == DBNull.Value ? EDSecurity.Unknown : (EDSecurity)((long)o);

                o = dr["eddb_updated_at"];
                EddbUpdatedAt = o == DBNull.Value ? 0 : (int)((long)o);

                o = dr["state"];
                State = o == DBNull.Value ? EDState.Unknown : (EDState)((long)o);

                o = dr["needs_permit"];
                NeedsPermit = o == DBNull.Value ? 0 : (int)((long)o);



            } catch (Exception ex) {
                System.Diagnostics.Trace.WriteLine("Exception SystemClass: " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Trace: " + ex.StackTrace);

            }
        } 
        #endregion
    }
}
