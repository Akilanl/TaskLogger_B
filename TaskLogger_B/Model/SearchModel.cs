using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaskLogger_B.Model
{
    public class SearchModel
    {
        [DataMember]
        public String FieldName { get; set; }

        [DataMember]
        public String SearchValue { get; set; }

        [DataMember]
        public Int32 SearchValueDataType { get; set; }

        [DataMember]
        public Boolean ExactlyMatch { get; set; }
    }
}
