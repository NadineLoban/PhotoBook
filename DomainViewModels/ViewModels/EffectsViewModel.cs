using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainViewModels.ViewModels
{
    public class EffectsViewModel
    {
        public String EffectShowName { get; set; }
        public String EffectName { get; set; }
        public Int32? DefaultValue { get; set; }
        public Int32? MinValue { get; set; }
        public Int32? MaxValue { get; set; }
    }
}
