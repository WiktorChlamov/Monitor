using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor.Validation
{
    internal class ParamsLengthValidation:Validation
    {
        public override object Handle(object request)
        {
            if ((request as string[]).Length != 3)
            {
                return $"Неправильное количество параметров";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
