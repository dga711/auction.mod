﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction.mod
{
	public enum ScrollsPostPriceType {
		LOWER, SUGGESTED, UPPER
	}
    public enum ScrollsPostDayType
    {
        one, three, seven, fourteen, thirty, hour
    }

   public class Settings
    {
       public string autoAuctionRoom = "";
       public int auctionScrollsMessagesCounter = 0;
       public bool doAutoTrade = false;
        public string AucBotMode = ""; 
        public bool waitForAuctionBot = false;
        public bool actualTrading = false;
        public long tradeCardID = 0;
        public bool addedCard = false;
        public int bidgold = 0;

       private static Settings instance;

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }

        private Settings()
        {
        }

        public bool shownumberscrolls;
        public string rowscalestring = "10";
        public float rowscale = 1.0f;
        public bool showsugrange;
        public bool wtsroundup = true;
        public int wtsroundmode = 0;
        public bool roundwts = false;
        public bool wtbroundup = false;
        public int wtbroundmode = 0;
        public bool roundwtb = false;
        public ScrollsPostDayType scrollspostday = 0;
		public ScrollsPostPriceType wtsGeneratorPriceType = ScrollsPostPriceType.UPPER, wtbGeneratorPriceType = ScrollsPostPriceType.LOWER;
		public ScrollsPostPriceType wtsAHpriceType = ScrollsPostPriceType.SUGGESTED, wtbAHpriceType = ScrollsPostPriceType.SUGGESTED, wtsAHpriceType2 = ScrollsPostPriceType.SUGGESTED, wtbAHpriceType2 = ScrollsPostPriceType.SUGGESTED;

        public string spampreventtime = "";
        public int spamprevint=0;

        public string dataset = "auc";

        public void loadsettings(double deleteTime)
        {

            string text = System.IO.File.ReadAllText(Helpfunktions.Instance.ownmodfolder + "settingsauc.txt");
            string[] txt = text.Split(';');
            foreach (string t in txt)
            {
                string setting = t.Split(' ')[0];
                string value = "";
                if (t.Split(' ').Length == 2)
                {
                    value = t.Split(' ')[1];
                }
                if (setting.Equals("spam"))
                {
                    spampreventtime = value;
                    if (spampreventtime != "") spamprevint = Convert.ToInt32(spampreventtime);
                    if (spamprevint > deleteTime) { spampreventtime = ((int)deleteTime).ToString(); spamprevint = (int)deleteTime; }
                }
                if (setting.Equals("numbers"))
                {
                    shownumberscrolls = Convert.ToBoolean(value);
                }
                if (setting.Equals("range"))
                {
                    showsugrange = Convert.ToBoolean(value);
                }
                if (setting.Equals("rowscale"))
                {
                    rowscalestring = value;
                    if (rowscalestring != "") { rowscale = (float)Convert.ToDouble(rowscalestring) / 10f; } else { rowscale = 1.0f; }
                    if (rowscale > 2f) { rowscale = 2f; rowscalestring = "20"; }
                    if (rowscale < 0.5f) { rowscale = .5f; }
                }
                if (setting.Equals("sround"))
                {
                    roundwts = Convert.ToBoolean(value);
                }
                if (setting.Equals("sroundu"))
                {
                    wtsroundup = Convert.ToBoolean(value);
                }
                if (setting.Equals("sroundm"))
                {
                    wtsroundmode = Convert.ToInt32(value);
                }
                if (setting.Equals("bround"))
                {
                    roundwtb = Convert.ToBoolean(value);
                }
                if (setting.Equals("broundu"))
                {
                    wtbroundup = Convert.ToBoolean(value);
                }
                if (setting.Equals("broundm"))
                {
                    wtbroundmode = Convert.ToInt32(value);
                }
                if (setting.Equals("takegens"))
                {
					wtsGeneratorPriceType = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("takegenb"))
                {
					wtbGeneratorPriceType = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("takeahs1"))
                {
                    wtsAHpriceType = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("takeahs2"))
                {
					wtsAHpriceType2 = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("takeahb1"))
                {
					wtbAHpriceType = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("takeahb2"))
                {
					wtbAHpriceType2 = (ScrollsPostPriceType)Convert.ToInt32(value);
                }
                if (setting.Equals("scrollpostday"))
                {
                    scrollspostday = (ScrollsPostDayType)Convert.ToInt32(value);
                }
                if (setting.Equals("dataset"))
                {
                    value = (String.IsNullOrEmpty(value) ? "auc" : value);
                    dataset = value;
                    Helpfunktions.Instance.setOwnAucPath(Helpfunktions.Instance.ownmodfolder + dataset + System.IO.Path.DirectorySeparatorChar);
                }
            }

        }

        public void resetsettings() 
        {
            spampreventtime = "";
            spamprevint = 0;
            shownumberscrolls = false;
            showsugrange = false;
            rowscalestring = "10";
            rowscale = 1f;
            roundwts = false;
            wtsroundup = true;
            wtsroundmode = 0;
            roundwtb = false;
            wtbroundup = false;
            wtbroundmode = 0;
			wtsGeneratorPriceType = ScrollsPostPriceType.SUGGESTED;
			wtbGeneratorPriceType = ScrollsPostPriceType.LOWER;
			wtsAHpriceType = wtsAHpriceType = wtsAHpriceType2 = wtbAHpriceType2 = ScrollsPostPriceType.SUGGESTED;
            scrollspostday = ScrollsPostDayType.one;
            dataset = "auc";
        }

        public void savesettings()
        {
            string text = "";
            text = text + "spam " + spampreventtime + ";";
            text = text + "numbers " + shownumberscrolls.ToString() + ";";
            text = text + "range " + showsugrange.ToString() + ";";
            text = text + "rowscale " + rowscalestring + ";";
            text = text + "sround " + roundwts.ToString() + ";";
            text = text + "sroundu " + wtsroundup.ToString() + ";";
            text = text + "sroundm " + wtsroundmode.ToString() + ";";
            text = text + "bround " + roundwtb.ToString() + ";";
            text = text + "broundu " + wtbroundup.ToString() + ";";
            text = text + "broundm " + wtbroundmode.ToString() + ";";
            text = text + "takegens " + (int)wtsGeneratorPriceType + ";";
			text = text + "takegenb " + (int)wtbGeneratorPriceType + ";";
			text = text + "takeahs1 " + (int)wtsAHpriceType + ";";
			text = text + "takeahs2 " + (int)wtsAHpriceType2 + ";";
			text = text + "takeahb1 " + (int)wtbAHpriceType + ";";
			text = text + "takeahb2 " + (int)wtbAHpriceType2 + ";";
            text = text + "scrollpostday " + (int)scrollspostday + ";";
            text = text + "dataset " + dataset + ";";

            // create dataset directory
            System.IO.Directory.CreateDirectory(Helpfunktions.Instance.ownmodfolder + dataset);
            Helpfunktions.Instance.setOwnAucPath(Helpfunktions.Instance.ownmodfolder + dataset + System.IO.Path.DirectorySeparatorChar);

            System.IO.File.WriteAllText(Helpfunktions.Instance.ownmodfolder + "settingsauc.txt", text);
        }


    }
}
