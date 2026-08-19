using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waste_inv_application.Helpers
{
    public class UserSession
    {

        // Thông tin cơ bản từ sfcom_waste_users
        public static int CurrentUid { get; set; }
        public static string CurrentUsername { get; set; }

        // Thông tin tổng tồn kho lũy kế của User
        public static long QuantityWasteTotal { get; set; }
        public static long WeightWasteTotal { get; set; }

        // Kiểm tra trạng thái đăng nhập
        public static bool IsLoggedIn => CurrentUid > 0 && !string.IsNullOrEmpty(CurrentUsername);

        // Lưu thông tin khi đăng nhập thành công
        public static void Login(int uid, string username, long qtyTotal = 0, long weightTotal = 0)
        {
            CurrentUid = uid;
            CurrentUsername = !string.IsNullOrEmpty(username) ? username.Trim().ToUpper() : string.Empty;
            QuantityWasteTotal = qtyTotal;
            WeightWasteTotal = weightTotal;
        }

        // Xóa session khi Đăng xuất
        public static void Clear()
        {
            CurrentUid = 0;
            CurrentUsername = string.Empty;
            QuantityWasteTotal = 0;
            WeightWasteTotal = 0;
        }


    }
}
