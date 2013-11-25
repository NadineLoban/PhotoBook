var effects = [
    { NeedSlider: false, EffectName: "nofilter", EffectShowName: "No Filter" },
    { NeedSlider: false, EffectName: "grayscale", EffectShowName: "Grayscale" }, //no
    { NeedSlider: false, EffectName: "blackwhite", EffectShowName: "Blackwhite" }, //no
    { NeedSlider: true, EffectName: "sepia", EffectShowName: "Sepia", MinValue: 1, MaxValue: 100, DefaultValue: 80 }, // 1-100 def 80
    { NeedSlider: true, EffectName: "saturation", EffectShowName: "Saturation", MinValue: 1, MaxValue: 100, DefaultValue: 80 }, // -100-100 def 80
    { NeedSlider: true, EffectName: "hue", EffectShowName: "Hue", MinValue: 1, MaxValue: 100, DefaultValue: 80 },// -100-100 def 80
    { NeedSlider: true, EffectName: "oil_paint", EffectShowName: "Oil paint", MinValue: 0, MaxValue: 100, DefaultValue: 30 }, // 0-100 def 30
    { NeedSlider: true, EffectName: "vignette", EffectShowName: "Vignette", MinValue: 0, MaxValue: 100, DefaultValue: 20 }, //0-100 def 20
    { NeedSlider: true, EffectName: "pixelate", EffectShowName: "Pixalate", MinValue: 1, MaxValue: 100, DefaultValue: 5 }, //1-200 def 5
    { NeedSlider: true, EffectName: "gradient_fade", EffectShowName: "Gradient fade", MinValue: 0, MaxValue: 100, DefaultValue: 20 }, //0-100 def 20
    { NeedSlider: true, EffectName: "blur", EffectShowName: "Blur", MinValue: 1, MaxValue: 2000, DefaultValue: 100 }, //1-2000 def 100
    { NeedSlider: false, EffectName: "improve", EffectShowName: "Improve" }, //no
    { NeedSlider: true, EffectName: "tilt_shift", EffectShowName: "Tilt shift", MinValue: 1, MaxValue: 100, DefaultValue: 20 }, // 1-100 def 20
    { NeedSlider: true, EffectName: "sharpen", EffectShowName: "Sharpen", MinValue: 1, MaxValue: 2000, DefaultValue: 100 }, // 1-2000 def 100
    { NeedSlider: true, EffectName: "unsharp_mask", EffectShowName: "Unsharp mask", MinValue: 1, MaxValue: 2000, DefaultValue: 100 }, // 1-2000 def 100
    {NeedSlider: true, EffectName: "red", EffectShowName: "Red", MinValue: -100, MaxValue: 100, DefaultValue: 0}, // -100-100 def 0
    {NeedSlider: true, EffectName: "blue", EffectShowName: "Blue", MinValue: -100, MaxValue: 100, DefaultValue: 0}, // -100-100 def 0
    {NeedSlider: true, EffectName: "green", EffectShowName: "Green", MinValue: -100, MaxValue: 100, DefaultValue: 0}, // -100-100 def 0
    { NeedSlider: true, EffectName: "contrast", EffectShowName: "Contrast", MinValue: -100, MaxValue: 100, DefaultValue: 0 }, // -100-100 def 0
    { NeedSlider: true, EffectName: "vibrance", EffectShowName: "Vibrance", MinValue: -100, MaxValue: 100, DefaultValue: 20 }, // -100-100 def 20
    { NeedSlider: false, EffectName: "auto_color", EffectShowName: "Auto color" }, // no
    {NeedSlider: false, EffectName: "auto_brightness", EffectShowName: "Auto brightness"}, // no
    { NeedSlider: false, EffectName: "auto_contrast", EffectShowName: "Auto contrast" }, //no
    { NeedSlider: false, EffectName: "negate", EffectShowName: "Negate" }, //no
    { NeedSlider: true, EffectName: "fill_light", EffectShowName: "Fill light", MinValue: -100, MaxValue: 100, DefaultValue: 0 }, // -100-100 def 0
    {NeedSlider: true, EffectName: "blur_region", EffectShowName: "Blur region", MinValue: 1, MaxValue: 2000, DefaultValue: 100} //1-2000 def 100
];

$(selectEffect);

function selectEffect() {
    var effectName = $("#effect").val();
    var effect = getCurrentEffect(effectName);
    if (effect.NeedSlider) {
        $('#amount').show();
        $('#slider-vertical').show();
        
        $('#slider-vertical').slider({
            orientation: "vertical",
            range: "min",
            min: effect.MinValue,
            max: effect.MaxValue,
            value: effect.DefaultValue,
            slide: function (event, ui) {
                $("#amount").val(ui.value);
            }
        });
        $('#amount').val(effect.DefaultValue);
    } else {
        $('#amount').hide();
        $('#slider-vertical').hide();
    }
}

function getCurrentEffect(name) {
    for (var i = 0; i < effects.length; i++) {
        if (effects[i].EffectName == name) {
            return effects[i];
        }
    }
    return null;
}

