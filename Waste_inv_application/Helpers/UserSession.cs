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

        // Thông tin tổng tồn kho phân tách theo loại rác của User
        public static long QtyGeneral { get; set; }
        public static long WeightGeneral { get; set; }
        public static long QtyWater { get; set; }
        public static long WeightWater { get; set; }

        public static bool IsLoggedOut { get; set; } = false;

        // Kiểm tra trạng thái đăng nhập
        public static bool IsLoggedIn => CurrentUid > 0 && !string.IsNullOrEmpty(CurrentUsername);

        // Lưu thông tin khi đăng nhập thành công với 4 chỉ số tồn kho mới
        public static void Login(int uid, string username, long qtyGeneral = 0, long weightGeneral = 0, long qtyWater = 0, long weightWater = 0)
        {
            CurrentUid = uid;
            CurrentUsername = !string.IsNullOrEmpty(username) ? username.Trim().ToUpper() : string.Empty;
            QtyGeneral = qtyGeneral;
            WeightGeneral = weightGeneral;
            QtyWater = qtyWater;
            WeightWater = weightWater;
        }

        // Xóa session khi Đăng xuất
        public static void Clear()
        {
            CurrentUid = 0;
            CurrentUsername = string.Empty;
            QtyGeneral = 0;
            WeightGeneral = 0;
            QtyWater = 0;
            WeightWater = 0;
        }
    }
}