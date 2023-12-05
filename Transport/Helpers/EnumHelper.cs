using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;

namespace Transport.Helpers
{
    internal class EnumHelper
    {
        public static List<string> GetEnumTypes(Type type)
        {
            
            var list = new List<string>();

            foreach (var e in Enum.GetValues(type))
            {
                list.Add(e.ToString()!);
            }

            return list;
        }
    }
}
