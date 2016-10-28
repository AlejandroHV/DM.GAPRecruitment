using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.UI.Controllers.BLL.Util
{
    public interface ISystemFail
    {

        string Message { get; set; }
        bool Error { get; set; }
        Exception Exception { get; set; }

    }


    public class SystemFail :ISystemFail
    {
        public string Message { get; set; }

        public bool Error { get; set; }

        public Exception Exception { get; set; }

        public SystemFail()
        {

            this.Error = false;
        }
    }
}