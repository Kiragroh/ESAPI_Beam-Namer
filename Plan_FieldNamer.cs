using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Windows.Controls;
using System.ComponentModel;

//[assembly: AssemblyFileVersion("2.0")]
[assembly: AssemblyVersion("2.1")]
[assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        const string SCRIPT_NAME = "BeamNamer";
        int BeamCount = 1;
        string newBeamId = "";
        string gantrydirection ="";
        string tableangle = "";
        string newBeamCount;

        public Script()
        {
        }

        public void Execute(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            if (context.PlanSetup == null)
            {
                MessageBox.Show("Please select a treatment plan");
            }
            // enable writing with this script.
            context.Patient.BeginModifications();

            // first beam loop is necessary to change all beam-IDs to unique ones (other than the script ants to produce). Otherwise you could get errors if a right named beam already exist.
            foreach (Beam b in context.PlanSetup.Beams)
            {
                newBeamCount = BeamCount<10? "0"+BeamCount.ToString(): BeamCount.ToString();
                newBeamId = "zzz"+newBeamCount;
                b.Id = newBeamId.Length > 16 ? newBeamId.Substring(0, 16) : newBeamId;
                BeamCount++;
            }
            //Treatment field-Loop
            BeamCount = 1;
            foreach (Beam b in context.PlanSetup.Beams.Where(x => !x.IsSetupField).OrderBy(y=> y.Id))
            {                
                gantrydirection = b.GantryDirection.ToString().ToLower() == "none" ? "" : (b.GantryDirection.ToString().ToLower() == "clockwise" ? " cw" : " ccw");
                tableangle = Math.Round(b.ControlPoints.First().PatientSupportAngle,0) == 0?"": " T"+ Math.Round(b.ControlPoints.First().PatientSupportAngle, 0);
                newBeamId = BeamCount + " G" + Math.Round(b.ControlPoints.First().GantryAngle, 0) + gantrydirection + tableangle;
                b.Id = newBeamId.Length > 16 ? newBeamId.Substring(0, 16) : newBeamId;
                BeamCount++;
            }
            //Setup field-Loop
            foreach (Beam b in context.PlanSetup.Beams.Where(x => x.IsSetupField).OrderBy(y => y.Id))
            {
                StructureSet ss = context.PlanSetup.StructureSet;
                gantrydirection = b.GantryDirection.ToString().ToLower() == "none" ? "" : (b.GantryDirection.ToString().ToLower() == "clockwise" ? " cw" : " ccw");
                tableangle = Math.Round(b.ControlPoints.First().PatientSupportAngle, 0) == 0 ? "" : " T" + Math.Round(b.ControlPoints.First().PatientSupportAngle, 0);
                newBeamId = "Setup " + Math.Round(b.ControlPoints.First().GantryAngle, 0) + gantrydirection + tableangle;
                b.Id = newBeamId.Length > 16 ? newBeamId.Substring(0, 16) : newBeamId;                          
            }
        }
     
    }
}
