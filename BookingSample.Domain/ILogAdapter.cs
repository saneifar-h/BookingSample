using System;

namespace BookingSample.Domain
{
    public interface ILogAdapter
    {
        void Debug(object obj);
        void Debug(object obj, Exception exp);

        void Error(object obj);
        void Error(object obj, Exception exp);

        void Fatal(object obj);
        void Fatal(object obj, Exception exp);

        void Info(object obj);
        void Info(object obj, Exception exp);

        void Warn(object obj);
        void Warn(object obj, Exception exp);
    }
}