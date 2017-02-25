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

        public static void main(String[] args)
        {
            IronSword sword = new IronSword();
            System.Console.WriteLine("Sword has"+sword.getMight()+" might.");
            IronLance lance = new IronLance();
            System.Console.WriteLine("Lance has" + lance.getMight() + " might.");
            IronAxe axe = new IronAxe();
            System.Console.WriteLine("Axe has" + axe.getMight() + " might.");
            Fire flames = new Fire();
            System.Console.WriteLine("Flames has" + flames.getMight() + " might.");
            Staff staff = new Staff();
            System.Console.WriteLine("Flames has" + staff.getMight() + " might.");
            Vulnerary vul = new Vulnerary();
            System.Console.WriteLine("Vul has "+vul.getMight()+" might and currently"+vul.getDurability()+" durability.");
            ItemManager mg = new ItemManager();
            mg.useVulnenary(vul);
            System.Console.WriteLine("Vul's durability is now at "+vul.getDurability());
            mg.useVulnenary(vul);
            System.Console.WriteLine("Vul's durability is now at " + vul.getDurability());
            mg.useVulnenary(vul);
            System.Console.WriteLine("Vul's durability is now at " + vul.getDurability());
            mg.useVulnenary(vul);
            System.Console.WriteLine("Vul's durability is now at " + vul.getDurability());
        }
    }
}
