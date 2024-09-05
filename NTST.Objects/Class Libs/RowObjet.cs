using System;
using System.Collections.Generic;
using System.Text;

namespace IC.ProgressNotesSCL.Objects
{
    public class RowObject
    {
        public List<FieldObject> Fields { get; set; }
        public string ParentRowId { get; set; }
        public string RowAction { get; set; }
        public string RowId { get; set; }
    }
}
