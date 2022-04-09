using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "In Asteptare")]
        Pending,
        
        [EnumMember(Value = "Plata acceptata")]
        PaymentReceived,

        [EnumMember(Value = "Plata Esuata")]
        PaymentFailed
    }
}