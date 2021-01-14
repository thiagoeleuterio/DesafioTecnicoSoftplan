using System.Runtime.Serialization;

namespace Calculo.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [DataContract]
    public struct ResultMessageResponse<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "errors")]
        public string[] Errors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "data")]
        public TResult Data { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success => !(Errors?.Length > 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        public ResultMessageResponse(TResult data, params string[] errors)
        {
            Data = data;
            Errors = errors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ResultMessageResponse<TResult>(TResult value)
        {
            return new ResultMessageResponse<TResult>(value, null);
        }
    }
}