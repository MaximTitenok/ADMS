using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADMS.Services
{
    internal static class GetStudyFormAndLevelNames
    {
        internal readonly static string[] StudForms = { 
            "FT OC (Full-time on-campuse)", 
            "FT E (Full-time evening)",
            "C E (Correspondence education)",
        };

        internal readonly static string[] StudLevels = {
            "Bachelor",
            "Top-up Bachelor",
            "Master",
            "PhD"
        };

        public static string GetStudyFormName(int type)
        {
            switch (type)
            {
                case 0:
                    {
                        return StudForms[0];
                    }
                case 1:
                    {
                        return StudForms[1];
                    }
                case 2:
                    {
                        return StudForms[2];
                    }
                default:
                    {
                        return "Unknown";
                    }
            }
        }
        public static string GetStudyLevelName(int type)
        {
            switch (type)
            {
                case 0:
                    {
                        return StudLevels[0];
                    }
                case 1:
                    {
                        return StudLevels[1];
                    }
                case 2:
                    {
                        return StudLevels[2];
                    }
                case 3:
                    {
                        return StudLevels[3];
                    }
                default:
                    {
                        return "Unknown";
                    }
            }
        }
    }
}
