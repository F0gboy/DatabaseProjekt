using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProjekt
{
    public class Character
    {
        public string name;
        public int lvl;
        public int order;
        public int stage;
        public int kills;
        public int death;


        public Character (string name, int lvl, int order, int stage, int kills, int death)
        {
            this.name = name;
            this.lvl = lvl; 
            this.order = order;
            this.stage = stage;
            this.kills = kills;
            this.death = death;
             
        }




    }

}
