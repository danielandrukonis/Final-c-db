using StudentsSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsSystem
{
    public class Lecture
    {

        public Guid Id { get; set; }
        public string Name { get; set; }


        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }


        public Lecture(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}