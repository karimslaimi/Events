using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Univ
{
    public class serviceUniversity : servicePattern<University>, IserviceUniversity
    {
        public List<University> GetAll1()
        {
            return this.GetMany(null,null).ToList();
        }
    }
}
