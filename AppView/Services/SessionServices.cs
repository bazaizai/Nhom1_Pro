using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class SessionServices
    {
        public static User GetObjFromSession(ISession session, string key)
        {
            //Lấy string Json từ Session
            var jsonData = session.GetString(key);
            if (jsonData == null) return null;
            //Chuyển đổi dữ liệu vừa lấy được sang dạng mong muốn.
            var obj = JsonConvert.DeserializeObject<User>(jsonData);// nếu null trả về một list rỗng
            return obj;
        }

        public static void SetObjToSession(ISession session, string key, object values)
        {
            var jsonData = JsonConvert.SerializeObject(values);
            session.SetString(key, jsonData);
        }
    }
}
