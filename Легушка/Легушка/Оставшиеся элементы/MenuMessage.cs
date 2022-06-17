using System;

namespace Легушка
{
    [Serializable]
    class MenuMessage
    {
        public int menuchoice;
        public bool activegame;
        public int skinchoice;
        public int coins;
        public MenuMessage()
        {
            menuchoice = 0;
            activegame = false;
            skinchoice = 0;
            coins = 0;
        }
    }
}
