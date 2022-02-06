using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor.Validation
{
    internal class ValidationChain
    {
        private ParamsLengthValidation _lengthValidation = new ParamsLengthValidation();
        private NameCheckParam _nameCheck = new NameCheckParam();
        private LifeTimeParamValidation _findingDelayValidation = new LifeTimeParamValidation();
        private DelayParamValidation _delayParamValidation = new DelayParamValidation();

        private void SetChain(out IHandler start)
        {
           _lengthValidation.SetNext(_nameCheck).SetNext(_findingDelayValidation).SetNext(_delayParamValidation);
            start = _lengthValidation;
        }

        public object StartValidation(object ob)
        {
            SetChain(out IHandler start);
            return start.Handle(ob);
        }
    }
}
