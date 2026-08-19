using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waste_inv_application.Helpers
{
    public static class DbSchema
    {
        public static class Users
        {
            // Tên Bảng
            public const string TABLE_NAME = "sfcom_waste_users";

            // Tên các Cột
            public const string COL_Uid = "user_id";
            public const string COL_Username = "username";
            public const string COL_Password = "password_user";

            // Các cột tổng tồn kho lũy kế (Mới chuyển từ Wastes sang)
            public const string COL_Quantity_waste_total = "quantity_waste_total";
            public const string COL_Weight_waste_total = "weight_waste_total";

            // Cột Audit Log
            public const string COL_Created_by = "created_by";
            public const string COL_Creation_date = "creation_date";
            public const string COL_Last_updated_by = "last_updated_by";
            public const string COL_Last_update_date = "last_update_date";
            public const string COL_Last_update_login = "last_update_login";
        }

        public static class Wastes
        {
            // Tên Bảng
            public const string TABLE_NAME = "sfcom_waste_inv";

            // Tên các Cột
            public const string COL_Uid = "id";
            public const string COL_Department = "department";
            public const string COL_Date_report = "date_report";
            public const string COL_Type_waste = "type_waste";
            public const string COL_Quantity_waste = "quantity_waste";
            public const string COL_Weight_waste = "weight_waste";
            public const string COL_Is_cancel = "is_cancel";
            public const string COL_Action = "action";

            // Cột Audit Log
            public const string COL_Created_by = "created_by";
            public const string COL_Creation_date = "creation_date";
            public const string COL_Last_updated_by = "last_updated_by";
            public const string COL_Last_update_date = "last_update_date";
            public const string COL_Last_update_login = "last_update_login";
        }
    }
}