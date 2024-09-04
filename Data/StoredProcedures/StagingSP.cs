using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.StoredProcedures
{
    public class StagingSP
    {
        public static string usp_Staging_IsTrackOut = "usp_Staging_IsTrackOut";

        //For test purposes
        public static string usp_Staging_IsTrackOut_Test = "usp_Staging_IsTrackOut_Test";
        public static string usp_Material_Details = "usp_MTL_Material_Details";
        public static string usp_Material_Customer = "usp_MTL_MaterialType_GetCustomer";
        public static string usp_Check_Param = "usp_MTL_Parameter_Check";
        public static string usp_Material_History = "usp_MTL_Material_History_Endpoint";

        // New Material Thawing Flow
        public static string usp_Material_InOut_Endpoint = "usp_MTL_Material_InOut_Endpoint";
        public static string usp_Material_LotMachine_Endpoint = "usp_MTL_Material_Lot_Machine_Usage_Endpoint";
    }
}