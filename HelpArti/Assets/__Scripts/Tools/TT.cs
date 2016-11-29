// Author: Pietro Polsinelli - http://designAGame.eu
// Twitter https://twitter.com/ppolsinelli
// All free as in free beer :-)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OL
{
    /// <summary>
    /// Also using those in DemiLib
    /// http://demigiant.github.io/apis/demilib/html/namespace_d_g_1_1_de_extensions.html
    /// </summary>
    public static class TT
    {
        //------------------------------------------------------------------------------------------------
        // Finite Numbers       

        public enum FinNum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }

        public enum RelativeFinNum
        {
            MinusThree = -3,
            MinusTwo = -2,
            MinusOne = -1,
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }


        public static FinNum RandomPosFinNum()
        {
            return (FinNum) Random.Range(1, 4);
        }

        public static FinNum RandomFinNum()
        {
            return (FinNum) Random.Range(0, 4);
        }

        //------------------------------------------------------------------------------------------------
        // Randomness

        public static T OneRndExcluding<T>(this IList<T> list, T excluded)
        {
            List<T> listToClone = list.Where(item => !item.Equals(excluded)).ToList();
            return listToClone[Random.Range(0, listToClone.Count)];
        }

        public static T OneRnd<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static int PosOrNegRND()
        {
            return (Random.Range(0f, 1f) > .5 ? 1 : -1);
        }

        public static bool BoolRND()
        {
            return PosOrNegRND()>0;
        }

        //N.B. Changes the list passed.
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //------------------------------------------------------------------------------------------------
        // Extensions

        public static bool Between(this int num, int lower, int upper, bool inclusive = true)
        {
            return Betweenf(num, lower, upper, inclusive);
        }

        public static bool Betweenf(this float num, float lower, float upper, bool inclusive = true)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public static bool IsEven(this int x)
        {
            return x%2 == 0;
        }

        public static bool Ex(this string s)
        {
            return s != null && s.Trim().Length > 0;
        }


        //------------------------------------------------------------------------------------------------
        // Shortcuts

        public static bool Ex(params string[] args)
        {
            bool ex = true;
            foreach (string s in args)
            {
                ex = ex && (s != null && s.Trim().Length > 0);
                if (!ex)
                    break;
            }
            return ex;
        }

        public static bool Ex<T>(IEnumerable<T> o)
        {
            bool result = false;
            if (o != null)
            {
                result = o.Any();
            }
            return result;
        }

        public static IEnumerable<T> EnumValues<T>()
        {
            return Enum.GetValues(typeof (T)).Cast<T>();
        }


        //------------------------------------------------------------------------------------------------
        //Colors

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        public static Color HexToColor(string hex)
        {
            byte r = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }


        //http://gamedev.stackexchange.com/questions/38536/given-a-rgb-color-x-how-to-find-the-most-contrasting-color-y
        public static Color ContrastColor(Color c)
        {
            Color ret = Color.white;

            float Y = 0.2126f*c.r + 0.7152f*c.g + 0.0722f*c.b;

            //float S = (Mathf.Max(c.r, c.g, c.b) - Mathf.Min(c.r, c.g, c.b)) / Mathf.Max(c.r, c.g, c.b);

            if (Y > .5f)
                ret = Color.black;
            return ret;
        }

        public static Color BrightenColor(Color c)
        {
            HSBColor hsbColor = new HSBColor(c);
            if (hsbColor.s < .5f)
            {
                hsbColor.s = .8f;
            }
            return hsbColor.ToColor();
        }


        //------------------------------------------------------------------------------------------------
        // Others

        public static Transform Search(this Transform target, string name)
        {
            if (target.name == name)
                return target;

            for (int i = 0; i < target.childCount; ++i)
            {
                var result = Search(target.GetChild(i), name);

                if (result != null)
                    return result;
            }

            return null;
        }

        public static string GetIP()
        {
            /*#if UNITY_EDITOR
                    return Network.player.ipAddress;
            #endif
            #if UNITY_IOS
                            return Network.player.ipAddress;
            #endif
            #if UNITY_ANDROID
                    return Network.player.ipAddress;
            #endif
            #if UNITY_STANDALONE
                            return Network.player.ipAddress;
            #endif*/
            return "Unknown IP";
        }

        public static GameObject FindGameObjectChildWithTag(GameObject parent, string tag)
        {
            Transform[] ch = parent.GetComponentsInChildren<Transform>();
            foreach (Transform c in ch)
            {
                if (c.tag == tag)
                    return c.gameObject;
            }
            return null;
        }

        public static Rect ScreenRect
        {
            get { return new Rect(-Screen.width/2f, -Screen.height/2f, Screen.width, Screen.height); }
        }
        
        //CSV reader from https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/

        private static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        private static char[] TRIM_CHARS = {'\"'};

        public static List<Dictionary<string, object>> ReadCSV(string file)
        {
            var list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load(file) as TextAsset;

            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            if (lines.Length <= 1) return list;

            var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {

                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }
                list.Add(entry);
            }
            return list;
        }

        public static void PlayNTimes(this AudioSource audioSource, int times, float volume)
        {
            audioSource.PlayOneShot(audioSource.clip, 1);
            times--;
            if (times > 0)
                DOVirtual.DelayedCall(audioSource.clip.length, () =>
                {
                    PlayNTimes(audioSource, times, volume);
                });
        }

        public static FinNum DecreaseFinNum(this FinNum fn)
        {
            if (fn == FinNum.Zero)
                return fn;
            FinNum decreaseFinNum = (FinNum) ((int)fn - 1);
            return decreaseFinNum;
        }

        public static FinNum IncreaseFinNum(this FinNum fn)
        {
            if (fn == FinNum.Three)
                return fn;
            return (FinNum)((int)fn + 1);
        }

        public static FinNum ModifyFinNum(FinNum num, int delta)
        {
            if (delta == 0)
                return num;
            if (delta > 0)
            {
                for (int x = 0; x < delta; x++)
                    num = num.IncreaseFinNum();
            } else if (delta < 0)
            {
                for (int x = 0; x > delta; x--)
                    num = num.DecreaseFinNum();
            }
            return num;
        }

        public static RelativeFinNum DecreaseRelativeFinNum(this RelativeFinNum fn)
        {
            if (fn == RelativeFinNum.MinusThree)
                return fn;
            RelativeFinNum decreaseFinNum = (RelativeFinNum)((int)fn - 1);
            return decreaseFinNum;
        }

        public static RelativeFinNum IncreaseRelativeFinNum(this RelativeFinNum fn)
        {
            if (fn == RelativeFinNum.Three)
                return fn;
            return (RelativeFinNum)((int)fn + 1);
        }

        public static RelativeFinNum ModifyRelativeFinNum(RelativeFinNum num, int delta)
        {
            if (delta == 0)
                return num;
            if (delta > 0)
            {
                for (int x = 0; x < delta; x++)
                    num = num.IncreaseRelativeFinNum();
            }
            else if (delta < 0)
            {
                for (int x = 0; x > delta; x--)
                    num = num.DecreaseRelativeFinNum();
            }
            return num;
        }



        public static string RemoveSpecialCharacter(string str, char special)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c != special)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
