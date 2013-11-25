using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainViewModels.ViewModels
{
    public class AfterUploadPhotoViewModel
    {
        public String PhotoUrl { get; set; }
        public int ImageDataId { get; set; }
        public String PublicId { get; set; }

        public List<EffectsViewModel> EffectsViewModels { get; set; }

        public AfterUploadPhotoViewModel()
        {
            EffectsViewModels = new List<EffectsViewModel>
                {
                    new EffectsViewModel {EffectName = "nofilter", EffectShowName = "No filter"}, // 1-100 def 80
                    new EffectsViewModel {EffectName = "grayscale", EffectShowName = "Grayscale"}, //no
                    new EffectsViewModel {EffectName = "blackwhite", EffectShowName = "Blackwhite"}, //no
                    new EffectsViewModel {EffectName = "sepia", EffectShowName = "Sepia", MinValue = 1, MaxValue = 100, DefaultValue = 80}, // 1-100 def 80
                    new EffectsViewModel {EffectName = "saturation", EffectShowName = "Saturation", MinValue = 1, MaxValue = 100, DefaultValue = 80}, // -100-100 def 80
                    new EffectsViewModel {EffectName = "hue", EffectShowName = "Hue", MinValue = 1, MaxValue = 100, DefaultValue = 80},// -100-100 def 80
                    new EffectsViewModel {EffectName = "oil_paint", EffectShowName = "Oil paint", MinValue = 0, MaxValue = 100, DefaultValue = 30}, // 0-100 def 30
                    new EffectsViewModel {EffectName = "vignette", EffectShowName = "Vignette", MinValue = 0, MaxValue = 100, DefaultValue = 20}, //0-100 def 20
                    new EffectsViewModel {EffectName = "pixelate", EffectShowName = "Pixalate", MinValue = 1, MaxValue = 100, DefaultValue = 5}, //1-200 def 5
                    new EffectsViewModel {EffectName = "gradient_fade", EffectShowName = "Gradient fade", MinValue = 0, MaxValue = 100, DefaultValue = 20}, //0-100 def 20
                    new EffectsViewModel {EffectName = "blur", EffectShowName = "Blur", MinValue = 1, MaxValue = 2000, DefaultValue = 100}, //1-2000 def 100
                    new EffectsViewModel {EffectName = "improve", EffectShowName = "Improve"}, //no
                    new EffectsViewModel {EffectName = "tilt_shift", EffectShowName = "Tilt shift", MinValue = 1, MaxValue = 100, DefaultValue = 20}, // 1-100 def 20
                    new EffectsViewModel {EffectName = "sharpen", EffectShowName = "Sharpen", MinValue = 1, MaxValue = 2000, DefaultValue = 100}, // 1-2000 def 100
                    new EffectsViewModel {EffectName = "unsharp_mask", EffectShowName = "Unsharp mask", MinValue = 1, MaxValue = 2000, DefaultValue = 100}, // 1-2000 def 100
                    new EffectsViewModel {EffectName = "red", EffectShowName = "Red", MinValue = -100, MaxValue = 100, DefaultValue = 0}, // -100-100 def 0
                    new EffectsViewModel {EffectName = "blue", EffectShowName = "Blue", MinValue = -100, MaxValue = 100, DefaultValue = 0}, // -100-100 def 0
                    new EffectsViewModel {EffectName = "green", EffectShowName = "Green", MinValue = -100, MaxValue = 100, DefaultValue = 0}, // -100-100 def 0
                    new EffectsViewModel {EffectName = "contrast", EffectShowName = "Contrast", MinValue = -100, MaxValue = 100, DefaultValue = 0}, // -100-100 def 0
                    new EffectsViewModel {EffectName = "vibrance", EffectShowName = "Vibrance", MinValue = -100, MaxValue = 100, DefaultValue = 20}, // -100-100 def 20
                    new EffectsViewModel {EffectName = "auto_color", EffectShowName = "Auto color"}, // no
                    new EffectsViewModel {EffectName = "auto_brightness", EffectShowName = "Auto brightness"}, // no
                    new EffectsViewModel {EffectName = "auto_contrast", EffectShowName = "Auto contrast"}, //no
                    new EffectsViewModel {EffectName = "negate", EffectShowName = "Negate"}, //no
                    new EffectsViewModel {EffectName = "fill_light", EffectShowName = "Fill light", MinValue = -100, MaxValue = 100, DefaultValue = 0}, // -100-100 def 0
                    new EffectsViewModel {EffectName = "blur_region", EffectShowName = "Blur region", MinValue = 1, MaxValue = 2000, DefaultValue = 100} //1-2000 def 100
                    
                };
        }
    }
}
