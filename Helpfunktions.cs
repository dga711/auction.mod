﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonFx.Json;
using UnityEngine;
using System.Reflection;

namespace Auction.mod
{
    class Helpfunktions
    {
        public FieldInfo targetchathightinfo;
        public ChatUI target = null;
        public MethodInfo hideInformationinfo;
        public FieldInfo buymen, sellmen;

        public Store storeinfo;
        public FieldInfo showBuyinfo;
        public FieldInfo showSellinfo;

        public bool chatisshown = false;
        public bool wtsmsgload = false;
        public bool wtbmsgload = false;
        public string ownaucpath = Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "auc" + System.IO.Path.DirectorySeparatorChar;
        public bool inauchouse = false;
        public bool wtsmenue=false;
        public bool generator=false;
        public bool settings = false;
        public string postmsgmsg = "";
        public bool postmsgontrading=false;
        public bool postmsggetnextroomenter = false;
        public bool showtradedialog=false;
        public int[] cardids;
        public string[] cardnames;
        public int[] cardImageid;
        public string[] cardType;

        public Dictionary<string, int> cardnamesToIndex = new Dictionary<string, int>();
        public Dictionary<int, int> cardidsToIndex = new Dictionary<int, int>();
        public Dictionary<string, int> cardnamesToImgidDic = new Dictionary<string, int>();
        public Dictionary<int, string> cardidsToCardnames = new Dictionary<int, string>();
        public Dictionary<string, int> cardnamesToID = new Dictionary<string, int>();

        public Dictionary<string, ChatUser> globalusers = new Dictionary<string, ChatUser>();
        public GUISkin cardListPopupSkin;
        public GUISkin cardListPopupGradientSkin;
        public GUISkin cardListPopupBigLabelSkin;
        public GUISkin cardListPopupLeftButtonSkin;
        public GUISkin lobbySkin;
        public GUIStyle chatLogStyle;

        public bool nicks = false;
        public List<nickelement> loadedscrollsnicks = new List<nickelement>();

        public int cardnameToArrayIndex(string name)
        { 
            int ret; 
            if (cardnamesToIndex.TryGetValue(name,out ret)) return ret;
            return -1; 
        }
        public int cardidToArrayIndex(int id) 
        {
            int ret;
            if (cardidsToIndex.TryGetValue(id, out ret)) return ret;
            return -1;
        }
        public int cardnametoimageid(string name) 
        {
            int ret;
            if (cardnamesToImgidDic.TryGetValue(name, out ret)) return ret;
            return -1; 
        }

        public void setskins(GUISkin cllps, GUISkin clpgs, GUISkin clpbls, GUISkin clplbs)
        {
            this.cardListPopupSkin = cllps;
            this.cardListPopupGradientSkin = clpgs;
            this.cardListPopupBigLabelSkin = clpbls;
            this.cardListPopupLeftButtonSkin = clplbs;

        }

        public void adjustskins(float fieldHeight)
        {
            this.cardListPopupBigLabelSkin.label.fontSize = (int)(fieldHeight / 1.7f);
            this.cardListPopupSkin.label.fontSize = (int)(fieldHeight / 2.5f);
        }

        public void setlobbyskin(GUISkin lby)
        {
            this.lobbySkin = lby;
        }

        public void setchatlogstyle(GUIStyle cls)
        {
            this.chatLogStyle = cls;
        }

        public void  setarrays(Message msg)
        {
            JsonReader jsonReader = new JsonReader();
            Dictionary<string, object> dictionary = (Dictionary<string, object>)jsonReader.Read(msg.getRawText());
            Dictionary<string, object>[] d = (Dictionary<string, object>[])dictionary["cardTypes"];
            this.cardids = new int[d.GetLength(0)];
            this.cardnames = new string[d.GetLength(0)];
            this.cardImageid = new int[d.GetLength(0)];
            this.cardType = new string[d.GetLength(0)];
            for (int i = 0; i < d.GetLength(0); i++)
            {
                this.cardids[i] = Convert.ToInt32(d[i]["id"]);
                this.cardnames[i] = d[i]["name"].ToString().ToLower();
                this.cardImageid[i] = Convert.ToInt32(d[i]["cardImage"]);
                this.cardType[i] = d[i]["kind"].ToString();
            }
            generatedictionarys();
        }

        public void readnicksfromfile()
        {
            if (this.nicks)
            {
                string[] lines = System.IO.File.ReadAllLines(this.ownaucpath + "nicauc.txt");
                foreach (string s in lines)
                {
                    if (s == "" || s == " ") continue;
                    string cardname = s.Split(':')[0];
                    if (this.cardnames.Contains(cardname.ToLower()))
                    {
                        string[] nickes = (s.Split(':')[1]).Split(',');
                        foreach (string n in nickes)
                        {
                            nickelement nele;
                            nele.nick = n.ToLower();
                            nele.cardname = cardname.ToLower();
                            this.loadedscrollsnicks.Add(nele);

                        }
                    }

                }


            }
        }
    
    
        private void generatedictionarys()
        {
            this.cardidsToIndex.Clear();
            this.cardnamesToIndex.Clear();
            this.cardnamesToImgidDic.Clear();
            this.cardidsToCardnames.Clear();
            this.cardnamesToID.Clear();
            for (int i = 0; i < cardids.Length; i++)
            {
                this.cardidsToIndex.Add(this.cardids[i], i);
                this.cardnamesToIndex.Add(cardnames[i], i);
                this.cardnamesToImgidDic.Add(cardnames[i], cardImageid[i]);
                this.cardidsToCardnames.Add(cardids[i], cardnames[i]);
                this.cardnamesToID.Add(cardnames[i], cardids[i]);
                
            }
        
        }
    
    }
}