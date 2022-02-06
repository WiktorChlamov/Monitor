using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor.Validation
{
    internal class LifeTimeParamValidation:Validation
    {
        public override object Handle(object request)
        {
            int number;
            if (!int.TryParse((request as string[])[1], out number))
            {
                return "Второй параметр должен быть целочисленным";
            }
            
            else
            {
                return base.Handle(request);
            }
        }
    }
}
