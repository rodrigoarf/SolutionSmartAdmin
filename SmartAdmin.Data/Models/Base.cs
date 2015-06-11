using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class Base
    {
        protected Guid? IdObject { get; set; }

        public int ID { get; set; }

        public Base()
        {
            IdObject = Guid.NewGuid();
        }

        [NotMapped]
        public Guid? IDLOGIC
        {
            get
            {
                return IdObject;
            }
            set
            {
                IdObject = value;
            }
        }
    }
}

