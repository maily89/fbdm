using System;
namespace FBD.Models
{
    public interface IIndex
    {
         string Formula { get; set; }
         string IndexID { get; set; }
         string IndexName { get; set; }
         bool LeafIndex { get; set; }
         string Unit { get; set; }
         string ValueType { get; set; }
    }
}
