using EliteModels;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Mappers {
    public class UserMapper {
        public static User Map(ParseUser puser) {
            User user = new User();
            user.UserName = puser.Username;
            user.ObjectId = puser.ObjectId;
            user.Email = puser.Email;
            return user;
        }
    }
}
