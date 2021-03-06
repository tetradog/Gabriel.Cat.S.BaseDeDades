﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{

    [AttributeUsage(System.AttributeTargets.Property,AllowMultiple =false)]
    public class RemoveSQL:System.Attribute
    {
        public enum Option
        {
            Null,
            Default,
            Cascade
        }

        public Option OptionOnDelete { get; private set; }

        public RemoveSQL(Option optionOnDelete=Option.Null)
        {
            OptionOnDelete = optionOnDelete;
        }
        public override string ToString()
        {
            return OptionOnDelete.ToString();
        }
    }
}
