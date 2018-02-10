using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaskLogger_B.Model
{
    public class Master
    {
        [DataMember]
        public int key { get; set; }
        [DataMember]
        public DateTime OfDate { get; set; }
        [DataMember]
        public int Task { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string WorkDone { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
        [DataMember]
        public double hoursSpent { get; set; }
        [DataMember]
        public int UserID { get; set; }
    }
}
