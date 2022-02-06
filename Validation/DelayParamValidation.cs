using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor.Validation
{
    internal class DelayParamValidation:Validation
    {
        public override object Handle(object request)
        {
            int number;
            if (!int.TryParse((request as string[])[2], out number))
            {
                return "Третий параметр должен быть целочисленным";
            }
            else if(number <= 0)
            {
                return "Третий параметр должен быть больше нуля";
            }

            else
            {
                return base.Handle(request);
            }
        }
    }
}
