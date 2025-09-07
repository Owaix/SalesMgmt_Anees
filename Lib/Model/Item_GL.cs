using System;

namespace Lib.Model
{
    public class Item_GL
    {
        public int RID { get; set; }
        public int RID2 { get; set; }
        public int TypeCode { get; set; }
        public DateTime GLDate { get; set; }
        public int AC_Code { get; set; }
        public int AC_Code2 { get; set; }
        public String Narration { get; set; }
        public float OP_Price { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
    }
}