using MediatR;
using System;

namespace Calculo.Api.App
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainNotification : IRequest, INotification
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type CommandType { get; private set; }

        #region Factory

        /// <summary>
        /// 
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="commandType"></param>
            /// <param name="description"></param>
            /// <returns></returns>
            public static DomainNotification Create(object commandType, string description)
            {
                return new DomainNotification()
                {
                    CommandType = commandType.GetType(),
                    Description = description
                };
            }
        }

        #endregion
    }
}
