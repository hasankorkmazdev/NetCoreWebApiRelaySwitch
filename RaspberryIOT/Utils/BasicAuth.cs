using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT.Utils
{
    public class BasicAuth
    {
        public  List<Guid> Authorized { get; set; } = new List<Guid>();

        public  bool isAuth(Guid key)
        {
            var q=Authorized.Where(x => x == key).FirstOrDefault();
            if (q==Guid.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Guid? CreateAuthKey()
        {
            Guid authorizedKey = Guid.NewGuid();
            Authorized.Add(authorizedKey);
            return authorizedKey;
        }
        public bool RemoveAuthKey(Guid key)
        {
            try
            {

           
            var q = Authorized.Where(x => x == key).FirstOrDefault();
            if (q==Guid.Empty)
            {
                return true;
            }
            else
            {
                Authorized.Remove(q);
                return true;
            }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
