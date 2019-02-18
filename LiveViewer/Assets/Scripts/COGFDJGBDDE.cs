using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ConsoleApp1
{

    public class COGFDJGBDDE
    {
        public static string LBKCAIMGDMM;
        

        public static T HAPNAIKFGNI<T>(string FPHBLOBOGBF, bool FNKPEJKDLAN = false, bool HDNBIDKLEMJ = false)
        {
            COGFDJGBDDE.LBKCAIMGDMM = (string)null;
            if (FNKPEJKDLAN)
                return JsonConvert.DeserializeObject<T>(FPHBLOBOGBF);
            if (HDNBIDKLEMJ)
                return JsonConvert.DeserializeObject<T>(FPHBLOBOGBF, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Binder = (SerializationBinder)new COGFDJGBDDE.BHCHLMNGHEO(),
                    Error = (EventHandler<ErrorEventArgs>)((CELMHMIHELO, NPCDNCKDGDB) =>
                   {
                       COGFDJGBDDE.LBKCAIMGDMM = NPCDNCKDGDB.ErrorContext.Error.Message;
                       NPCDNCKDGDB.ErrorContext.Handled = true;
                   })
                });
            return JsonConvert.DeserializeObject<T>(FPHBLOBOGBF, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Error = (EventHandler<ErrorEventArgs>)((CELMHMIHELO, NPCDNCKDGDB) =>
               {
                   COGFDJGBDDE.LBKCAIMGDMM = NPCDNCKDGDB.ErrorContext.Error.Message;
                   NPCDNCKDGDB.ErrorContext.Handled = true;
               })
            });
        }

        public static string JDNKNLNDGNB(object DEDGNAJHPAP, bool FNKPEJKDLAN = false)
        {
            if (FNKPEJKDLAN)
                return JsonConvert.SerializeObject(DEDGNAJHPAP);
            return JsonConvert.SerializeObject(DEDGNAJHPAP, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
        
        public class BHCHLMNGHEO : DefaultSerializationBinder
        {
            public virtual System.Type OPJGBCHGHGD(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "模型 {0} 设定")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Интервал")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type JLHCBAILINH(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == ".model.json")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Visualization")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type EKJMPDKLDBH(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Full Size")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Webコントロール")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type OOKDLGNAGKC(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "陰影")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Load data failed")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type BMMHEEIAIFI(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Flexible")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Live2D")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type PLKBPGKMMIA(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "ParamEyeROpen")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Manga 4")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type KIGIGMEMOEF(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "30FPS")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "キーバインディング - 表情")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type MKFJOPKPANJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "グループ")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "雪")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type INKFPCIACJH(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "上傳物品")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "８ビット")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type FMFMBOJOHDM(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "+")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Нет настраиваемого контента")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type CEFDGCLLKAJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "按键绑定")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "全屏")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type MEKBKLHIJKJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "模型")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Compat")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type HAMDPHBMBLN(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Отменить")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "[AVProVideo]HLSParser cannot parse stream ")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type OPLNOMNNGNK(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "壞掉的玻璃2")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Recent Events: ")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type EAMBCNACMBD(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "SetFocusRotation")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "текст")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type JPJGBLLDMOJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "costume")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Save")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type MFDOFNLPAIL(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "[")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "水平縮放")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public override System.Type BindToType(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "SettingData+ModelSetting")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "SettingData+ModelSetting[]")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type EOLKNADKFBF(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "/")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Array is empty, or not valid.")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type LNHGMDPJHGH(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "New Cell Shading")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "_UseYpCbCr")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type GNOLIFGCAFJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "小部件透明度")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "DefaultWallpaperOffsetEmulator")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type OJDOGKLAMMJ(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Deinitialise")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "ユーザー")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type FNNMBLDEMED(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "/")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "/")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type KEEGCONLBDB(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Глобальные настройки")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "init_param")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type POHEHFNPLLL(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "weight")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "连接出错，程序即将退出")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type MFOKLJMLLLN(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Array is empty, or not valid.")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "リズムに合わせてスケール")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type CCGONACEOLD(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "Bubble Auto Close")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "preview")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type JGEHFNFHLJI(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "秒の表示")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "Список изменений")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type EHJGONMDCOF(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "發射數量")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "オフセット")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }

            public virtual System.Type LJHLDPEFDDC(string FNOKAJJIDGE, string HLMFNOHNMNM)
            {
                if (HLMFNOHNMNM != null)
                {
                    if (HLMFNOHNMNM == "setup")
                        return typeof(ModelData.ModelSetting);
                    if (HLMFNOHNMNM == "ビデオ")
                        return typeof(ModelData.ModelSetting[]);
                }
                return base.BindToType(FNOKAJJIDGE, HLMFNOHNMNM);
            }
        }
    }
}
