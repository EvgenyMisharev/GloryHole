using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GloryHole
{
    public class GloryHoleSettings
    {
        public List<string> SelectedRevitLinkInstancesNames;
        public string IntersectionWallRectangularFamilySymbolName { get; set; }
        public string IntersectionWallRoundFamilySymbolName { get; set; }
        public string IntersectionFloorRectangularFamilySymbolName { get; set; }
        public string IntersectionFloorRoundFamilySymbolName { get; set; }
        public string HoleShapeButtonName { get; set; }
        public string RoundHolesPositionButtonName { get; set; }
        public string RoundHoleSizesUpIncrementValue { get; set; }
        public string RoundHolePositionIncrementValue { get; set; }

        public string PipeSideClearanceValue { get; set; }
        public string PipeTopBottomClearanceValue { get; set; }
        public string DuctSideClearanceValue { get; set; }
        public string DuctTopBottomClearanceValue { get; set; }
        public string CableTraySideClearanceValue { get; set; }
        public string CableTrayTopBottomClearanceValue { get; set; }
        public bool CombineHolesValue { get; set; }

        public GloryHoleSettings GetSettings()
        {
            GloryHoleSettings viewScheduleSheetSpreaderSettings = null;
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "GloryHoleSettings.xml";
            string assemblyPath = assemblyPathAll.Replace("GloryHole.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                using (FileStream fs = new FileStream(assemblyPath, FileMode.Open))
                {
                    XmlSerializer xSer = new XmlSerializer(typeof(GloryHoleSettings));
                    viewScheduleSheetSpreaderSettings = xSer.Deserialize(fs) as GloryHoleSettings;
                    fs.Close();
                }
            }
            else
            {
                viewScheduleSheetSpreaderSettings = new GloryHoleSettings();
            }

            return viewScheduleSheetSpreaderSettings;
        }

        public void SaveSettings()
        {
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "GloryHoleSettings.xml";
            string assemblyPath = assemblyPathAll.Replace("GloryHole.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                File.Delete(assemblyPath);
            }

            using (FileStream fs = new FileStream(assemblyPath, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(GloryHoleSettings));
                xSer.Serialize(fs, this);
                fs.Close();
            }
        }
    }
}
