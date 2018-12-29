using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDP
{
    public static class NumberToLettersConverter
    {

        private const int MASCULIN = 0;

        private const int FEMININ = 1;

        private static List<List<string>> _deLa1La9InLitere;

        private static List<List<string>> DeLa1La9InLitere
        {

            get
            {

                if (_deLa1La9InLitere == null)
                {

                    InitializeazaDeLa1La9InLitere();

                }

                return _deLa1La9InLitere;

            }

        }

        private static List<List<string>> _deLa11La19InLitere;

        private static List<List<string>> DeLa11La19InLitere
        {

            get
            {

                if (_deLa11La19InLitere == null)
                {

                    InitializeazaDeLa11La19InLitere();

                }

                return NumberToLettersConverter._deLa11La19InLitere;

            }

        }



        public static string Convert(decimal Nr)
        {

            //transforma un nr din cifre un litere



            decimal miliarde;

            decimal milioane;

            decimal mii;

            decimal lei;

            decimal bani;

            string litere = string.Empty;



            bool minus = false;

            if (Nr < 0)
            {

                Nr = Math.Abs(Nr);

                minus = true;

            }



            miliarde = Math.Floor(Nr / 1000000000m);

            milioane = Math.Floor((Nr - miliarde * 1000000000m) / 1000000m);

            mii = Math.Floor((Nr - miliarde * 1000000000m - milioane * 1000000m) / 1000m);

            lei = Math.Floor(Nr - miliarde * 1000000000m - milioane * 1000000m - mii * 1000m);

            bani = Math.Floor(Math.Abs((Nr - Math.Floor(Nr)) * 100m));



            if (miliarde != 0)
            {

                if (miliarde == 1)

                    litere += "un miliard ";

                else
                {

                    if (miliarde % 10 == 1)

                        litere += InLit(miliarde, MASCULIN) + " miliarde ";

                    else

                        litere += InLit(miliarde, FEMININ) + " miliarde ";

                }

            }

            if (milioane != 0)
            {

                if (milioane == 1)

                    litere += "un milion ";

                else
                {

                    if (milioane % 10 == 1)

                        litere += InLit(milioane, MASCULIN) + " milioane ";

                    else

                        litere += InLit(milioane, FEMININ) + " milioane ";

                }

            }

            if (mii != 0)
            {

                if (mii == 1)

                    litere += "una mie ";

                else

                    litere += InLit(mii, FEMININ) + " mii ";

            }

            if (lei != 0)

                litere += InLit(lei, MASCULIN);



            if (Math.Floor(Nr) == 1)

                litere = "unleu";

            else if (Math.Floor(Nr) > 0)

                litere += " lei";



            if (bani != 0)
            {

                if (Math.Floor(Nr) > 0)
                {

                    if (bani == 1m)

                        litere += "siunban";

                    else

                        litere += "si" + InLit(bani, MASCULIN) + "bani";

                }

                else
                {

                    if (bani == 1m)

                        litere += "unban";

                    else

                        litere += InLit(bani, MASCULIN) + "bani";

                }

            }

            return (minus == true ? "minus" : string.Empty) + strtran(litere, " ", string.Empty);



        }



        private static void InitializeazaDeLa11La19InLitere()
        {

            _deLa11La19InLitere = new List<List<string>>();

            InitializeazaDeLa11La19InLiterePentruGenulMasculin();

            InitializeazaDeLa11La19InLiterePentruGenulFeminin();

        }

        private static void InitializeazaDeLa11La19InLiterePentruGenulMasculin()
        {

            DeLa11La19InLitere.Add(new List<string>(9));

            DeLa11La19InLitere[MASCULIN].Add("unsprezece");

            DeLa11La19InLitere[MASCULIN].Add("doisprezece");

            DeLa11La19InLitere[MASCULIN].Add("treisprezece");

            DeLa11La19InLitere[MASCULIN].Add("paisprezece");

            DeLa11La19InLitere[MASCULIN].Add("cincisprezece");

            DeLa11La19InLitere[MASCULIN].Add("saisprezece");

            DeLa11La19InLitere[MASCULIN].Add("saptesprezece");

            DeLa11La19InLitere[MASCULIN].Add("optsprezece");

            DeLa11La19InLitere[MASCULIN].Add("nouasprezece");

        }

        private static void InitializeazaDeLa11La19InLiterePentruGenulFeminin()
        {

            DeLa11La19InLitere.Add(new List<string>(9));

            DeLa11La19InLitere[FEMININ].Add("unsprezece");

            DeLa11La19InLitere[FEMININ].Add("doisprezece");

            DeLa11La19InLitere[FEMININ].Add("treisprezece");

            DeLa11La19InLitere[FEMININ].Add("paisprezece");

            DeLa11La19InLitere[FEMININ].Add("cincisprezece");

            DeLa11La19InLitere[FEMININ].Add("saisprezece");

            DeLa11La19InLitere[FEMININ].Add("saptesprezece");

            DeLa11La19InLitere[FEMININ].Add("optsprezece");

            DeLa11La19InLitere[FEMININ].Add("nouasprezece");

        }

        private static void InitializeazaDeLa1La9InLitere()
        {

            _deLa1La9InLitere = new List<List<string>>();

            InitializeazaDeLa1La9PentruGenulMasculin();

            InitializeazaDeLa1la9PentruGenulFeminin();

        }

        private static void InitializeazaDeLa1La9PentruGenulMasculin()
        {

            DeLa1La9InLitere.Add(new List<string>(9));

            DeLa1La9InLitere[MASCULIN].Add("unu");

            DeLa1La9InLitere[MASCULIN].Add("doi");

            DeLa1La9InLitere[MASCULIN].Add("trei");

            DeLa1La9InLitere[MASCULIN].Add("patru");

            DeLa1La9InLitere[MASCULIN].Add("cinci");

            DeLa1La9InLitere[MASCULIN].Add("sase");

            DeLa1La9InLitere[MASCULIN].Add("sapte");

            DeLa1La9InLitere[MASCULIN].Add("opt");

            DeLa1La9InLitere[MASCULIN].Add("noua");

        }

        private static void InitializeazaDeLa1la9PentruGenulFeminin()
        {

            DeLa1La9InLitere.Add(new List<string>(9));

            DeLa1La9InLitere[FEMININ].Add("una");

            DeLa1La9InLitere[FEMININ].Add("doua");

            DeLa1La9InLitere[FEMININ].Add("trei");

            DeLa1La9InLitere[FEMININ].Add("patru");

            DeLa1La9InLitere[FEMININ].Add("cinci");

            DeLa1La9InLitere[FEMININ].Add("sase");

            DeLa1La9InLitere[FEMININ].Add("sapte");

            DeLa1La9InLitere[FEMININ].Add("opt");

            DeLa1La9InLitere[FEMININ].Add("noua");

        }

        private static string strtran(string sir, string ce_scot, string ce_bag)
        {

            return sir.Replace(ce_scot, ce_bag);

        }

        private static string InLit(decimal nr_pana_la_999, int gen)
        {

            //primeste ca parametrii un nr de trei cifre si

            //genul articularii , intoarce in litere ;

            //ex : 341-> trei sute patru zeci si unu / si una

            decimal ce = nr_pana_la_999;

            if (ce < 0 || ce > 999)

                throw new ArgumentException("nr_pana_la_999 trebuie sa fie intre 0 si 999");

            if (gen != MASCULIN && gen != FEMININ)

                throw new ArgumentException("parametrul gen nu poate fi decat 0 sau 1");

            decimal sute;

            decimal zeci;

            decimal unit;

            string text = string.Empty;

            sute = Math.Floor(ce / 100m);

            zeci = Math.Floor((ce - sute * 100m) / 10m);

            unit = Math.Floor(ce - 100m * sute - 10m * zeci);

            if (sute != 0)

                text = DeLa1La9InLitere[FEMININ][(int)sute - 1] + (sute == 1 ? " suta " : " sute ");

            if (zeci == 1)

                return text + (unit == 0 ? " zece " : DeLa11La19InLitere[gen][(int)unit - 1]);



            if (zeci > 1)
            {

                text += DeLa1La9InLitere[FEMININ][(int)zeci - 1] + " zeci ";

                if (unit != 0)

                    text += "si ";

            }



            if (unit != 0)

                text += DeLa1La9InLitere[gen][(int)unit - 1];

            return text;

        }

    }
}
