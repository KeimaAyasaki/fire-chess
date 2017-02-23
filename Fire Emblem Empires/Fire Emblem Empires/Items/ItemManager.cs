using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Items
{
    public class ItemManager
    {
        public Boolean checkVulneraryStatus(Vulnerary vul)
        {
            if (vul.getDurability() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void useVulnenary(Vulnerary vul)
        {
            if (checkVulneraryStatus(vul))
            {
                vul.setDurability(vul.getDurability()-1);
            }
        }
    }
}
