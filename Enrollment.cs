//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Day7ExeEFManageStudent
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enrollment
    {
        public int pkEnrollmentId { get; set; }
        public Nullable<decimal> finalGrade { get; set; }
        public Nullable<int> fkStudentId { get; set; }
        public Nullable<int> fkCourseId { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
