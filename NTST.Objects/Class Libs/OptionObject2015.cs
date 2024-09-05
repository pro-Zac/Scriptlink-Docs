using System;
using System.Collections.Generic;
using System.Text;

namespace IC.ProgressNotesSCL.Objects
{
    public class OptionObject2015
    {
        public string EntityID { get; set; }
        public double EpisodeNumber { get; set; }
        public double ErrorCode { get; set; }
        public string ErrorMesg { get; set; }
        public string Facility { get; set; }
        public List<FormObject> Forms { get; set; }
        public string NamespaceName { get; set; }
        public string OptionId { get; set; }
        public string OptionStaffId { get; set; }
        public string OptionUserId { get; set; }
        public string ParentNamespace { get; set; }
        public string ServerName { get; set; }
        public string SystemCode { get; set; }
        public string SessionToken { get; set; }
    }
}
