using System.Runtime.Serialization;

namespace Calculo.Integration.Tests.Helpers
{
    [DataContract]
    public struct Result<TResult>
    {
        [DataMember(Name = "errors")]
        public string[] Errors { get; set; }

        [DataMember(Name = "data")]
        public TResult Data { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }

    }
}