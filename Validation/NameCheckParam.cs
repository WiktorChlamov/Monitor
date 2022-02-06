using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor.Validation
{
    internal class NameCheckParam:Validation
    {
        public override object Handle(object request)
        {
            if ((request as string[])[0] == "")
            {
                return $"Неправильное имя";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
