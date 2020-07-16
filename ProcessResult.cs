using System;

namespace DDL_to_CSharp
{
    internal class ProcessResult
    {
        private ProcessResultEnum processResult;

        public ProcessResult(ProcessResultEnum processResult)
        {
            this.processResult = processResult;
        }

        public override string ToString()
        {
            return processResult.ToString();
        }

        public static explicit operator ProcessResult(ProcessResultEnum processResult)
        {
            switch(processResult)
            {
                case ProcessResultEnum.Ignore:
                    return new ProcessResult(processResult);
                case ProcessResultEnum.Read:
                    return new ProcessResult(processResult);
                default:
                    return new ProcessResult(ProcessResultEnum.Read);
            }
        }
    }
}